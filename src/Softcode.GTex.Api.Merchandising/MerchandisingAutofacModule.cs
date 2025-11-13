using Autofac;
using Softcode.GTex.Api;
using Softcode.GTex.Api.Providers;

namespace Softcode.GTex.Api.Merchandising
{
    /// <summary>
    /// 
    /// </summary>
    public class MerchandisingAutofacModule : Module
    {
     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
           // builder.Register(c => new MerchandisingDbContext(ConnectionString)).AsSelf();
           // builder.RegisterGeneric(typeof(MerchandisingRepository<>)).As(typeof(IMerchandisingRepository<>)).InstancePerDependency();
          //  builder.RegisterType<BuyerService>().As<IBuyerService>().InstancePerDependency();
        }
    }
}
