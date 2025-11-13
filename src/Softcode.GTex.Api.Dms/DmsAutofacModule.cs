using Autofac;
using Softcode.GTex.ApplicationService.Dms;
using Softcode.GTex.ApplicationService.Dms.FileProcessor;

namespace Softcode.GTex.Api.Dms
{
    /// <summary>
    /// 
    /// </summary>
    public class DmsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileService>().As<IFileService>().InstancePerDependency();
            builder.RegisterType<FileStorageService>().AsSelf().InstancePerDependency();
        }
    }
}
