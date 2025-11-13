using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration.Enums;
using Softcode.GTex.Data.FileStorage;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApplicationService.Dms.FileProcessor
{
    public class FileStorageService : BaseService<FileStorageService>
    {
        private readonly IFileStorageRepository fileStorageRepository;
        public FileStorageService(IApplicationServiceBuilder applicationServiceBuilder, IFileStorageRepository fileStorageRepository): base(applicationServiceBuilder)
        {
            this.fileStorageRepository = fileStorageRepository;
        }

        public IFileProcessor FileProcessor {
            get {

                return GetFileProcessor();
            }
        }

        private IFileProcessor GetFileProcessor()
        {

            DocumentStorageType documentStorageType = DocumentStorageType.Db;

            if (documentStorageType == DocumentStorageType.Db)
            {
                IStorageConfigution storageConfigution = new DbStorageConfiguration();
                return new DbFileProcessor(storageConfigution, fileStorageRepository, HostingEnvironment);
            }

            //if (documentStorageType == DocumentStorageType.FileSystem)
            //{
            //    IStorageConfigution storageConfigution = new FileSystemStorageConfiguration();
            //    return new DbFileProcessor(storageConfigution);
            //}

            //if (documentStorageType == DocumentStorageType.Cloud)
            //{
            //    IStorageConfigution storageConfigution = new DbStorageConfiguration();
            //    return new DbFileProcessor(storageConfigution);
            //}

            throw new SoftcodeInvalidDataException("Invalid file information");
        }
    }
}
