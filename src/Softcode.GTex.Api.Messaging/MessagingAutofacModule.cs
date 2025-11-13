

using Autofac;
using Softcode.GTex.ApplicantionService.Messaging;
using Softcode.GTex.ApploicationService;

namespace Softcode.GTex.Api.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class MessagingAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>().InstancePerDependency();
            builder.RegisterType<ServerSettingService>().As<IServerSettingService>().InstancePerDependency();
         
        }
    }
}
