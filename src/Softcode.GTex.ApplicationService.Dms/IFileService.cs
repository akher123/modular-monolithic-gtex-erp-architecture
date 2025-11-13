using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicationService.Dms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms
{
    public interface IFileService
    {
        Task<LoadResult> GetDocumentsAsync(DataSourceLoadOptionsBase options);

        Task<List<AttachedFileModel>> GetDocumentsByEntityAsync(int entityTypeId, int entityId);

        Task<DocumentMetadataModel> GetDocumentByIdAsync(int id);
        Task<string> GetDownloadFileNameAsync(int id);
        Task<string> GetDownloadFileNameAsync(Guid key);

        Task<bool> SaveDocumentsAsync(int id, DocumentMetadataModel documentMetadata);
        Task<bool> SaveDocumentsByEntityAsync(int entityTypeId, int entityId, List<AttachedFileModel> models);


        Task<DownloadFileModel> DownloadLatestFileAsync(int documentMetadataId);
        Task<DownloadFileModel> DownloadFileAsync(int documentStoreFileId);
        Task<DownloadFileModel> DownloadFileAsync(Guid key);
        Task<DownloadFileModel> DownloadPublicFileAsync(string key);
        Task<bool> DeleteDocumentAsync(int id);
        Task<bool> DeleteDocumentsAsync(List<int> ids);
    }
}
