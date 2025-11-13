using Autofac;
using Softcode.GTex.Api.Providers;

namespace Softcode.GTex.Api.Accounting
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountingAutofacModule : Module
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new AccountingDbContext(ConnectionString)).AsSelf();
           
            //builder.RegisterGeneric(typeof(AccountingRepository<>)).As(typeof(IAccountingRepository<>)).InstancePerDependency();
            //builder.RegisterType<CompanySectorService>().As<ICompanySectorService>().InstancePerDependency();
            //builder.RegisterType<CostcenterService>().As<ICostcenterService>().InstancePerDependency();
            //builder.RegisterType<VoucherTypeService>().As<IVoucherTypeService>().InstancePerDependency();

        }
    }
}
