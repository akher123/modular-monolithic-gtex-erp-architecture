using Softcode.GTex.Data.FileStorage.Models;

namespace Softcode.GTex.Data.FileStorage
{
    public class FileStorageRepository : BaseRepository<FileStorageDbContext, FileStore>, IFileStorageRepository
    {
        public FileStorageRepository(FileStorageDbContext context, ILoggedInUserService loggedInUserService) 
            : base(context, loggedInUserService)
        {
        }
    }
}
