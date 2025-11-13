using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.Web.Api.ApplicationService;
using Softcode.GTex.ApplicantionService.Messaging;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Data;
using Softcode.GTex.Data.FileStorage;

namespace Softcode.GTex.Web.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register Entity Framework
            DbContextOptionsBuilder<ApplicationDbContext> applicationDbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(ItmConfigurations.ApplicationConnectionString);
            builder.RegisterType<ApplicationDbContext>().WithParameter("options", applicationDbContextBuilder.Options).InstancePerLifetimeScope();

            DbContextOptionsBuilder<FileStorageDbContext> fileStorageDbContextBuilder = new DbContextOptionsBuilder<FileStorageDbContext>().UseSqlServer(ItmConfigurations.FileStorageConnectionString);
            builder.RegisterType<FileStorageDbContext>().WithParameter("options", fileStorageDbContextBuilder.Options).InstancePerLifetimeScope();

            //builder.Register(c => new ApplicationDbContext(ConnectionString)).AsSelf();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


            builder.RegisterType<StoredProcedureRepository>().As<IStoredProcedureRepository>().InstancePerDependency();
            builder.RegisterType<FileStorageRepository>().As<IFileStorageRepository>().InstancePerDependency();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterType<LoggedInUserService>().As<ILoggedInUserService>().InstancePerDependency();
            

            builder.RegisterType<ApplicationServiceBuilder>().As<IApplicationServiceBuilder>().SingleInstance();

            //services.AddTransient<LoggedInUserService, LoggedInUserService>();

            builder.RegisterType<EntityService>().As<IEntityService>().InstancePerDependency();


            
            builder.RegisterType<HangfireService>().As<IHangfireService>().InstancePerDependency();
            builder.RegisterType<EmailJobQueueService>().As<IEmailJobQueueService>().InstancePerDependency();
            builder.RegisterType<EmailQueueService>().As<IEmailQueueService>().InstancePerDependency();
            

            //builder.RegisterType<CustomPasswordValidator>().As(typeof(IPasswordValidator<>)).InstancePerDependency();
            //builder.RegisterType<Itm.ApplicationService.LinkMenuTemplates.TypeAndCategoryTemplate>().As<ILinkTemplate>().InstancePerDependency();
            //builder.RegisterType<Itm.Crm.ApplicationService.TypesAndCategories.TypeAndCategoryTemplate>().As<ILinkTemplate>().InstancePerDependency();
            //builder.RegisterType<Itm.Hrm.ApplicationService.TypesAndCategories.TypeAndCategoryTemplate>().As<ILinkTemplate>().InstancePerDependency();
            //builder.RegisterType<LoggedInUserService>().As<ILoggedInUserService>().InstancePerDependency();
            //builder.RegisterType<ILoggedInUserService, LoggedInUserService>();


        }
    }
}
