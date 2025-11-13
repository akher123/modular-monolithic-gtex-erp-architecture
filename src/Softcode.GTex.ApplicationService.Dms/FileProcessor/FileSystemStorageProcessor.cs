using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms.FileProcessor
{
    public class FileSystemStorageProcessor : BaseFileProcessor, IFileProcessor
    {
        public FileSystemStorageProcessor(IStorageConfigution storageConfigution) 
            : base(storageConfigution)
        {
        }

        public Task<bool> DeleteAsync(List<Guid> fileIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> DownloadAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UploadAsync(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
