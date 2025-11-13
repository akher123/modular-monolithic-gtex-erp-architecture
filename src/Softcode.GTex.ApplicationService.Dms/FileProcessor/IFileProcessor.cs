using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms.FileProcessor
{
    public interface IFileProcessor
    {
        Task<Guid> UploadAsync(string filePath);
        Task<byte[]> DownloadAsync(Guid fileId);
        Task<bool> DeleteAsync(List<Guid> fileIds);
        Task<bool> DeleteAsync(Guid fileId);
    }
}
