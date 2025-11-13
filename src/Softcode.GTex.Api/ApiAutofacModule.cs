using Autofac;
using Softcode.GTex.ApploicationService;

namespace Softcode.GTex.Api
{
    public class ApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BusinessProfileService>().As<IBusinessProfileService>().InstancePerDependency();
            builder.RegisterType<SystemConfigurationService>().As<ISystemConfigurationService>().InstancePerDependency();
            builder.RegisterType<SecurityProfileService>().As<ISecurityProfileService>().InstancePerDependency();

            builder.RegisterType<CustomCategoryService>().As<ICustomCategoryService>().InstancePerDependency();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerDependency();
            builder.RegisterType<ApplicationPageService>().As<IApplicationPageService>().InstancePerDependency();
            builder.RegisterType<AddressDatabaseService>().As<IAddressDatabaseService>().InstancePerDependency();
            builder.RegisterType<ApplicationMenuService>().As<IApplicationMenuService>().InstancePerDependency();
            builder.RegisterType<ApplicationCacheService>().As<IApplicationCacheService>().InstancePerDependency();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerDependency();
            builder.RegisterType<PhotoService>().As<IPhotoService>().InstancePerDependency();
            builder.RegisterType<CommunicationService>().As<ICommunicationService>().InstancePerDependency();

            builder.RegisterType<BusinessCategoryService>().As<IBusinessCategoryService>().InstancePerDependency();
            builder.RegisterType<ContactService>().As<IContactService>().InstancePerDependency();
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerDependency();
            builder.RegisterType<BusinessUnitService>().As<IBusinessUnitService>().InstancePerDependency();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerDependency();
            builder.RegisterType<BusinessProfileSiteService>().As<IBusinessProfileSiteService>().InstancePerDependency();

            builder.RegisterType<CostCentreService>().As<ICostCentreService>().InstancePerDependency();
            /*
             *
             *
             *  private readonly IUserService userService;
                private readonly IBusinessProfileService businessProfileService;
                private readonly ITypeAndCategoryService customCategoryService;
                private readonly ISharedService sharedService;
                private readonly ISystemConfigurationService systemConfig;
                private readonly ISecurityProfileService securityProfile;
             * 
             */
        }
    }
}
