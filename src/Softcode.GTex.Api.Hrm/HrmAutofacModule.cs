using Autofac;
using Softcode.GTex.ApplicationService.Hrm;

namespace Softcode.GTex.Api.Hrm
{
    public class HrmAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerDependency();
        }
    }
}