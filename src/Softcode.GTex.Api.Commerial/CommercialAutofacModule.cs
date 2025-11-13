using Autofac;
using Softcode.GTex.Api.Providers;

namespace Softcode.GTex.Api.Commerial
{
    /// <summary>
    /// 
    /// </summary>
    public class CommercialAutofacModule : Module
    {
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
           // builder.Register(c => new CommercialDbContext(ConnectionString)).AsSelf();
          //  builder.RegisterGeneric(typeof(CommercialRepository<>)).As(typeof(ICommercialRepository<>)).InstancePerDependency();
          //  builder.RegisterType<LetterOfCreditService>().As<ILetterOfCreditService>().InstancePerDependency();
        }
    }
}
