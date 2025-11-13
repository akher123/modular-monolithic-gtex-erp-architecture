using Microsoft.AspNetCore.Hosting;
using Softcode.GTex.Data.FileStorage;
using Softcode.GTex.Data.FileStorage.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms.FileProcessor
{
    public class DbFileProcessor : BaseFileProcessor, IFileProcessor
    {
        private readonly IFileStorageRepository fileStorageRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public DbFileProcessor(IStorageConfigution storageConfigution, IFileStorageRepository fileStorageRepository, IHostingEnvironment hostingEnvironment)
            : base(storageConfigution)
        {
            this.fileStorageRepository = fileStorageRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<byte[]> DownloadAsync(Guid fileId)
        {
            if (!await fileStorageRepository.ExistsAsync(x => x.Id == fileId))
            {
                throw new SoftcodeNotFoundException("File not found");
            }

            return fileStorageRepository.WhereAsync(x => x.Id == fileId).Result.FirstOrDefault().FileContent;
        }

        public async Task<Guid> UploadAsync(string filePath)
        {
            FileStore file = new FileStore();
            file.FileContent = GetDocumentByte(filePath);

            await fileStorageRepository.CreateAsync(file);

            return file.Id;
            
        }


        /// <summary>
        /// Get image file by file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetDocumentByte(string fileName)
        {
            byte[] file = null;
            string folderName = "tempfilestore";

            string webRootPath = hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            if (Directory.Exists(newPath))
            {
                string fullPath = Path.Combine(newPath, fileName);
                 
                using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }                
            }

            return file;
        }

        public async Task<bool> DeleteAsync(List<Guid> fileIds)
        {
            return await fileStorageRepository.DeleteAsync(f => fileIds.Contains(f.Id))>0;
        }

        public async Task<bool> DeleteAsync(Guid fileId)
        {
            return await fileStorageRepository.DeleteAsync(f => f.Id == fileId) > 0;
        }
    }
}
