
using Softcode.GTex.ApplicationService.Crm;

using Autofac;
namespace Softcode.GTex.Api.Crm
{
    public class CrmAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerDependency();
        }
    }
}
