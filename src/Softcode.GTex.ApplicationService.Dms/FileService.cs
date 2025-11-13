using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApplicationService.Dms.FileProcessor;
using Softcode.GTex.ApplicationService.Dms.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration.Enums;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms
{
    public class FileService : BaseService<FileService>, IFileService
    {
        private readonly IRepository<DocumentMetadata> documentMetadataRepository;
        private readonly IRepository<DocumentFileStore> documentFileStoreRepository;
        private readonly FileStorageService fileStorageService;

        public FileService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<DocumentMetadata> documentMetadataRepository
            , IRepository<DocumentFileStore> documentFileStoreRepository
            , FileStorageService fileStorageService)
            : base(applicationServiceBuilder)
        {
            this.documentMetadataRepository = documentMetadataRepository;
            this.documentFileStoreRepository = documentFileStoreRepository;
            this.fileStorageService = fileStorageService;
        }

        public async Task<LoadResult> GetDocumentsAsync(DataSourceLoadOptionsBase options)
        {
            options.Select = new[] { "Id", "Title", "BusinessProfile", "DocumentFor", "Entity", "FileName", "Description", "DateUploaded", "DocumentType", "UploadedBy", "Publickey" };

            var docMetaData = documentMetadataRepository.AsQueryable();
            if (!LoggedInUser.IsSuperAdmin)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                docMetaData = docMetaData.Where(x => x.BusinessProfileId == businessProfileId);
            }


            var query = from file in documentFileStoreRepository.AsQueryable()                         
                        join p in docMetaData.Include(f => f.EntityType)
                        .Include(f => f.BusinessProfile)
                        .Include(f => f.DocumentType)
                        .Include(f => f.CreatedByContact) on file.DocumentMetadataId equals p.Id

                        group file by file.DocumentMetadataId into grp
                        let MaxVersionNumber = grp.Max(g => g.VersionNumber)

                        from p in grp
                        where p.VersionNumber == MaxVersionNumber
                        select new
                        {
                            p.DocumentMetadata.Id,
                            p.DocumentMetadata.Title,
                            BusinessProfile = p.DocumentMetadata.BusinessProfile.CompanyName,
                            DocumentFor = p.DocumentMetadata.EntityType.Name,
                            Entity = "",
                            p.FileName,
                            DocumentType = p.DocumentMetadata.DocumentType.Name,
                            p.DocumentMetadata.Description,
                            UploadedBy = p.CreatedByContact.FirstName + " " + p.CreatedByContact.LastName,
                            DateUploaded = p.CreatedDateTime,
                            Publickey = p.Publickey
                        };


           

            return await Task.Run(() => DataSourceLoader.Load(query, options));
            
        }

        public async Task<List<AttachedFileModel>> GetDocumentsByEntityAsync(int entityTypeId, int entityId)
        {
            return await Task.Run(() => documentMetadataRepository.Where(d => d.EntityTypeId == entityTypeId && d.EntityId == entityId)
                 .Include(f => f.DocumentFileStores).ToList()
                 .Select(x =>
                 {
                     var latestFile = x.DocumentFileStores.OrderByDescending(v => v.VersionNumber).First();

                     return new AttachedFileModel
                     {
                         FileName = latestFile.FileName,
                         FileSizeInKb = latestFile.FileSizeInKb,
                         MimeType = latestFile.MimeType,
                         OrginalFileName = latestFile.OrginalFileName,
                         Title = x.Title,
                         Description = x.Description,
                         LastModifiedDate = x.LastUpdatedDateTime,
                         Id = latestFile.Id,
                         Version = latestFile.VersionNumber,
                         Extension = latestFile.Extension
                     };
                 }).ToList());

        }


        public async Task<DocumentMetadataModel> GetDocumentByIdAsync(int id)
        {
            if (id == 0)
            {
                return new DocumentMetadataModel { BusinessProfileId = this.LoggedInUser.DefaultBusinessProfileId };
            }
            DocumentMetadata dbDocument = await documentMetadataRepository.Where(d => d.Id == id).Include(d => d.DocumentFileStores).ThenInclude(c => c.CreatedByContact).FirstOrDefaultAsync();

            DocumentMetadataModel model = new DocumentMetadataModel();
            model.BusinessProfileId = dbDocument.BusinessProfileId;
            model.DocumentTypeId = dbDocument.DocumentTypeId;
            model.EntityId = dbDocument.EntityId;
            model.EntityTypeId = dbDocument.EntityTypeId;
            model.Title = dbDocument.Title;
            model.Description = dbDocument.Description;

            var file = dbDocument.DocumentFileStores.OrderByDescending(s => s.VersionNumber).FirstOrDefault();
            if (file != null)
            {
                AttachedFileModel attachModel = new AttachedFileModel();

                attachModel.FileName = file.FileName;
                attachModel.FileSizeInKb = file.FileSizeInKb;
                attachModel.MimeType = file.MimeType;
                attachModel.OrginalFileName = file.OrginalFileName;
                attachModel.Title = dbDocument.Title;
                attachModel.Description = dbDocument.Description;
                attachModel.LastModifiedDate = file.LastUpdatedDateTime;
                attachModel.Id = file.Id;
                attachModel.Version = file.VersionNumber;
                attachModel.Extension = file.Extension;

                model.files.Add(attachModel);

                model.Revisions.AddRange(dbDocument.DocumentFileStores
                    .Where(f => f.VersionNumber < file.VersionNumber)
                    .OrderByDescending(s => s.VersionNumber)
                    .Select(f => new AttachedFileModel
                    {
                        FileName = f.FileName,
                        FileSizeInKb = f.FileSizeInKb,
                        MimeType = f.MimeType,
                        OrginalFileName = f.OrginalFileName,
                        Title = dbDocument.Title,
                        Description = dbDocument.Description,
                        LastModifiedDate = f.LastUpdatedDateTime,
                        Id = f.Id,
                        Extension = f.Extension,
                        Version = f.VersionNumber,
                        CreatedBy = f.CreatedByContact.DisplayName
                    }).ToList());

            }

            return model;
        }

        public async Task<bool> SaveDocumentsAsync(int id, DocumentMetadataModel model)
        {
            if (id > 0)
            {
                DocumentMetadata dbDoc = documentMetadataRepository.Where(d => d.Id == id).Include(d => d.DocumentFileStores).FirstOrDefault();
                await documentMetadataRepository.UpdateAsync(BuildDocument(dbDoc, model, model.files.First()));
            }
            else
            {
                foreach (AttachedFileModel attach in model.files)
                {
                    DocumentMetadata doc = new DocumentMetadata();
                    await documentMetadataRepository.CreateAsync(BuildDocument(doc, model, attach));
                }
            }
            return true;
        }

        private DocumentMetadata BuildDocument(DocumentMetadata doc, DocumentMetadataModel model, AttachedFileModel attach)
        {

            doc.BusinessProfileId = model.BusinessProfileId;
            doc.DocumentTypeId = model.DocumentTypeId;
            doc.EntityId = model.EntityId;
            doc.EntityTypeId = model.EntityTypeId;


            doc.Title = attach.Title;

            if (string.IsNullOrEmpty(attach.Description))
            {
                doc.Description = model.Description;
            }
            else
            {
                doc.Description = attach.Description;
            }

            if (attach.Id == 0)
            {
                doc.DocumentFileStores.Add(new DocumentFileStore
                {
                    StorageTypeId = (int)DocumentStorageType.Db,
                    FileId = fileStorageService.FileProcessor.UploadAsync(attach.OrginalFileName).Result,
                    Extension = attach.Extension,
                    FileName = attach.FileName,
                    FileSizeInKb = attach.FileSizeInKb,
                    MimeType = attach.MimeType,
                    OrginalFileName = attach.OrginalFileName,
                    VersionNumber = doc.DocumentFileStores.Select(v => v.VersionNumber).DefaultIfEmpty(0).Max() + 1
                });
            }
            else
            {
                var dbDocStore = doc.DocumentFileStores.OrderByDescending(v => v.VersionNumber).First();
                dbDocStore.FileName = attach.FileName;
            }
            return doc;
        }


        public async Task<bool> SaveDocumentsByEntityAsync(int entityTypeId, int entityId, List<AttachedFileModel> models)
        {
            //Delete 
            List<DocumentMetadata> deletedDoc = new List<DocumentMetadata>();
            foreach (var dbFile in documentMetadataRepository
                .Where(x => x.EntityTypeId == entityTypeId && x.EntityId == entityId)
                .Include(x => x.DocumentFileStores))
            {
                if (!models.Any(x => dbFile.DocumentFileStores.Any(y => y.Id == x.Id)))
                {
                    deletedDoc.Add(dbFile);
                }

            }

            documentMetadataRepository.Remove(deletedDoc);

            //Create
            var newFiles = models.Where(m => m.IsDirty && m.Id < 1)
                 .Select(f =>
                  {
                      DocumentFileStore doc = new DocumentFileStore
                      {
                          FileName = f.FileName,
                          FileSizeInKb = f.FileSizeInKb,
                          MimeType = f.MimeType,
                          OrginalFileName = f.OrginalFileName,
                          Id = f.Id,
                          Extension = f.Extension,
                          VersionNumber = f.Version,
                          FileId = fileStorageService.FileProcessor.UploadAsync(f.OrginalFileName).Result,
                          StorageTypeId = (int)DocumentStorageType.Db,
                      };

                      //if (doc.Id > 0)
                      //{
                      doc.DocumentMetadata = new DocumentMetadata
                      {
                          EntityId = entityId,
                          EntityTypeId = entityTypeId,
                          IsArchived = false,
                          IsInternal = false,
                          BusinessProfileId = LoggedInUser.DefaultBusinessProfileId,
                          IsPublic = false,
                          Title = doc.FileName,
                          Description = f.Description
                      };

                      //}

                      return doc;
                  }).ToList();


            documentFileStoreRepository.CreateRange(newFiles);

            //Edit
            foreach (var file in models.Where(d => d.Id > 0 && d.IsDirty))
            {

                var dbFile = documentFileStoreRepository.Where(x => x.Id == file.Id).Include(x => x.DocumentMetadata).FirstOrDefault();
                dbFile.FileName = file.FileName;
                dbFile.DocumentMetadata.Description = file.Description;
                dbFile.DocumentMetadata.Title = file.Title;
            }


            return await documentFileStoreRepository.SaveChangesAsync() > 0;

        }


        public async Task<string> GetDownloadFileNameAsync(int documentMetadataId)
        {
            if (documentMetadataId < 1)
            {
                throw new SoftcodeArgumentMissingException("Invalid file");
            }

            DocumentFileStore documentFileStore = await documentFileStoreRepository.Where(x => x.DocumentMetadataId == documentMetadataId).OrderByDescending(x => x.VersionNumber).FirstOrDefaultAsync();

            if (documentFileStore != null && string.IsNullOrWhiteSpace(documentFileStore.FileName))
            {
                throw new SoftcodeInvalidDataException("Invalid file name");
            }

            return documentFileStore.FileName;
        }

        public async Task<string> GetDownloadFileNameAsync(Guid key)
        {
            if (key == Guid.Empty)
            {
                throw new SoftcodeArgumentMissingException("Invalid file");
            }

            DocumentFileStore documentFileStore = await documentFileStoreRepository.Where(x => x.FileId == key).FirstOrDefaultAsync();

            if (documentFileStore != null && string.IsNullOrWhiteSpace(documentFileStore.FileName))
            {
                throw new SoftcodeInvalidDataException("Invalid file name");
            }

            return documentFileStore.FileName;
        }

        public async Task<DownloadFileModel> DownloadLatestFileAsync(int documentMetadataId)
        {
            if (documentMetadataId < 1)
            {
                throw new SoftcodeArgumentMissingException("Invalid file");
            }

            DocumentMetadata documentMetadata = await documentMetadataRepository.Where(x => x.Id == documentMetadataId).Include(x => x.DocumentFileStores).FirstOrDefaultAsync();

            if (documentMetadata == null)
            {
                throw new SoftcodeNotFoundException("Document not found");
            }

            DocumentFileStore documentFileStore = documentMetadata.DocumentFileStores.OrderByDescending(x => x.VersionNumber).FirstOrDefault();

            if (documentFileStore == null)
            {
                throw new SoftcodeNotFoundException("File not found");
            }

            return new DownloadFileModel
            {
                File = await fileStorageService.FileProcessor.DownloadAsync(documentFileStore.FileId.Value),
                MimeType = documentFileStore.MimeType,
                FileName = documentFileStore.FileName + documentFileStore.Extension
            };
        }

        public async Task<DownloadFileModel> DownloadFileAsync(int documentStoreFileId)
        {
            if (documentStoreFileId < 1)
            {
                throw new SoftcodeArgumentMissingException("Invalid file");
            }

            DocumentFileStore documentFileStore = await documentFileStoreRepository.Where(x => x.Id == documentStoreFileId).FirstOrDefaultAsync();


            if (documentFileStore == null)
            {
                throw new SoftcodeNotFoundException("File not found");
            }

            return new DownloadFileModel
            {
                File = await fileStorageService.FileProcessor.DownloadAsync(documentFileStore.FileId.Value),
                MimeType = documentFileStore.MimeType,
                FileName = documentFileStore.FileName + documentFileStore.Extension
            };
        }

        public async Task<DownloadFileModel> DownloadFileAsync(Guid key)
        {
            if (Guid.Empty == key)
            {
                throw new SoftcodeArgumentMissingException("Invalid file");
            }

            DocumentFileStore documentFileStore = await documentFileStoreRepository.Where(x => x.FileId == key).FirstOrDefaultAsync();


            if (documentFileStore == null)
            {
                throw new SoftcodeNotFoundException("File not found");
            }

            return new DownloadFileModel
            {
                File = await fileStorageService.FileProcessor.DownloadAsync(documentFileStore.FileId.Value),
                MimeType = documentFileStore.MimeType,
                FileName = documentFileStore.FileName + documentFileStore.Extension
            };
        }

        public async Task<DownloadFileModel> DownloadPublicFileAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new SoftcodeArgumentMissingException("Invalid file");
            }

            string keyValue = Encryption.ConvertHexToString(key, System.Text.Encoding.Unicode);

            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new SoftcodeInvalidDataException("Invalid file");
            }

            string[] values;

            values = keyValue.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 2)
            {
                throw new SoftcodeInvalidDataException("Invalid data");
            }


            Guid fileId = values[0].ToGuid();
            int id = values[1].ToInt();


            DocumentFileStore documentFileStore = await documentFileStoreRepository.Where(x => x.FileId.Value == fileId && x.Id == id).FirstOrDefaultAsync();


            if (documentFileStore == null)
            {
                throw new SoftcodeNotFoundException("File not found");
            }

            return new DownloadFileModel
            {
                File = await fileStorageService.FileProcessor.DownloadAsync(documentFileStore.FileId.Value),
                MimeType = documentFileStore.MimeType,
                FileName = documentFileStore.FileName + documentFileStore.Extension
            };
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            if (id < 1)
            {
                throw new SoftcodeArgumentMissingException("document not found");
            }

            var doc = documentMetadataRepository.Where(d => d.Id == id).Include(x => x.DocumentFileStores).FirstOrDefault();

            if (doc == null)
            {
                throw new SoftcodeArgumentMissingException("document not found");
            }

            await fileStorageService.FileProcessor.DeleteAsync(doc.DocumentFileStores.Where(f => f.FileId != null).Select(f => f.FileId.Value).ToList());


            documentFileStoreRepository.Remove(doc.DocumentFileStores.ToList());



            return await documentMetadataRepository.DeleteAsync(t => t.Id == id) > 0;
        }

        public async Task<bool> DeleteDocumentsAsync(List<int> ids)
        {
            if (ids.Count < 1)
            {
                throw new SoftcodeArgumentMissingException("document not found");
            }
            foreach (int id in ids)
            {
                var doc = documentMetadataRepository.Where(d => d.Id == id).Include(x => x.DocumentFileStores).FirstOrDefault();

                if (doc == null)
                {
                    throw new SoftcodeArgumentMissingException("document not found");
                }

                await fileStorageService.FileProcessor.DeleteAsync(doc.DocumentFileStores.Where(f => f.FileId != null).Select(f => f.FileId.Value).ToList());
                documentFileStoreRepository.Remove(doc.DocumentFileStores.ToList());

                await documentMetadataRepository.DeleteAsync(t => t.Id == id);
            }

            return true;
        }
    }
}
