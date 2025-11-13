using Softcode.GTex.Data.Mappings;
using Softcode.GTex.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Softcode.GTex.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>//, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AccessLog> AccessLogs { get; set; }        
        public virtual DbSet<ApplicationMenu> ApplicationMenus { get; set; }
        public virtual DbSet<ApplicationPage> ApplicationPages { get; set; }
        public virtual DbSet<ApplicationPageAction> ApplicationPageActions { get; set; }
        public virtual DbSet<ApplicationPageDetailField> ApplicationPageDetailFields { get; set; }
        public virtual DbSet<ApplicationPageGroup> ApplicationPageGroups { get; set; }
        public virtual DbSet<ApplicationPageListField> ApplicationPageListFields { get; set; }
        public virtual DbSet<ApplicationPageNavigation> ApplicationPageNavigations { get; set; }
        public virtual DbSet<ApplicationPageService> ApplicationPageServices { get; set; }
        public virtual DbSet<ApplicationRoleRight> ApplicationRoleRights { get; set; }
        public virtual DbSet<BusinessCategory> BusinessCategories { get; set; }
        
        public virtual DbSet<BusinessCategoryType> BusinessCategoryTypes { get; set; }
        public virtual DbSet<BusinessProfile> BusinessProfiles { get; set; }
        public virtual DbSet<BusinessProfileSite> BusinessProfileSites { get; set; }
        public virtual DbSet<BusinessUnit> BusinessUnit { get; set; }
        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<CommunicationFileStore> CommunicationFileStores { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyOperatingState> CompanyOperatingStates { get; set; }
        public virtual DbSet<CompanyRelationshipType> CompanyRelationshipTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactBusinessProfile> ContactBusinessProfiles { get; set; }
        public virtual DbSet<ContactOperatingCity> ContactOperatingCities { get; set; }
        public virtual DbSet<ContactSpecialisation> ContactSpecialisations { get; set; }
        public virtual DbSet<CostCentre> CostCentre { get; set; }
        
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CustomCategory> CustomCategories { get; set; }
        public virtual DbSet<CustomCategoryType> CustomCategoryTypes { get; set; }
        public virtual DbSet<CustomCategoryMapType> CustomCategoryMapTypes { get; set; }
        public virtual DbSet<CustomCategoryMapTypeOption> CustomCategoryMapTypeOptions { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DocumentFileStore> DocumentFileStores { get; set; }
        public virtual DbSet<DocumentMetadata> DocumentMetadatas { get; set; }
        public virtual DbSet<EmailAttachment> EmailAttachments { get; set; }
        public virtual DbSet<EmailJobQueue> EmailJobQueues { get; set; }
        public virtual DbSet<EmailQueue> EmailQueues { get; set; }
        public virtual DbSet<EmailRecipient> EmailRecipients { get; set; }
       
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeCostCentre> EmployeeCostCentre { get; set; }
        public virtual DbSet<EmployeeSite> EmployeeSite { get; set; }
        public virtual DbSet<EntityContact> EntityContacts { get; set; }
        public virtual DbSet<EntityLayoutTemplate> EntityLayoutTemplates { get; set; }
        public virtual DbSet<EntityType> EntityTypes { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<FileStoreSetting> FileStoreSettings { get; set; }
        public virtual DbSet<Photo> Photoes { get; set; }
        public virtual DbSet<PostalCode> PostalCodes { get; set; }
        public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }
        public virtual DbSet<RecordInfo> RecordInfos { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SecurityConfiguration> SecurityConfigurations { get; set; }
        public virtual DbSet<SecurityProfile> SecurityProfiles { get; set; }
        public virtual DbSet<EmailServer> Servers { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<SystemEntity> SystemEntities { get; set; }
        public virtual DbSet<SystemEntityRight> SystemEntityRights { get; set; }
        public virtual DbSet<SystemEntityRightDependency> SystemEntityRightDependencies { get; set; }
        public virtual DbSet<SystemModule> SystemModules { get; set; }
        public virtual DbSet<TimeZone> TimeZones { get; set; }

        public virtual DbSet<UserBusinessProfile> UserBusinessProfile { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("security");
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new AddressMapping());
          



            modelBuilder.ApplyConfiguration(new AccessLogMapping());
            modelBuilder.ApplyConfiguration(new ApplicationMenuMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageListFieldMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageActionMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageDetailFieldMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageGroupMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageNavigationMapping());
            modelBuilder.ApplyConfiguration(new ApplicationPageServiceMapping());
            modelBuilder.ApplyConfiguration(new ApplicationRoleRightMapping());
            modelBuilder.ApplyConfiguration(new BusinessCategoryMapping());
            modelBuilder.ApplyConfiguration(new BusinessCategoryMapTypeMapping());
            modelBuilder.ApplyConfiguration(new BusinessCategoryTypeMapping());
            modelBuilder.ApplyConfiguration(new BusinessProfileMapping());
            modelBuilder.ApplyConfiguration(new BusinessProfileSiteMapping());
            modelBuilder.ApplyConfiguration(new BusinessProfileAddressMapping());
            modelBuilder.ApplyConfiguration(new BusinessProfileDepartmentMapping());
            modelBuilder.ApplyConfiguration(new BusinessUnitMapping());
            modelBuilder.ApplyConfiguration(new CommunicationMapping());
            modelBuilder.ApplyConfiguration(new CommunicationFileStoreMapping());
            modelBuilder.ApplyConfiguration(new CompanyMapping());
            modelBuilder.ApplyConfiguration(new CompanyDepartmentMapping());
            modelBuilder.ApplyConfiguration(new CompanyOperatingStateMapping());
            modelBuilder.ApplyConfiguration(new CompanyRelationshipTypeMapping());
            modelBuilder.ApplyConfiguration(new ContactMapping());
            modelBuilder.ApplyConfiguration(new ContactAddressMapping());
            modelBuilder.ApplyConfiguration(new ContactOperatingCityMapping());
            modelBuilder.ApplyConfiguration(new ContactSpecialisationMapping());
            modelBuilder.ApplyConfiguration(new ContactBusinessProfileMapping());
            modelBuilder.ApplyConfiguration(new CostCentreMapping());
            modelBuilder.ApplyConfiguration(new CountryMapping());
            modelBuilder.ApplyConfiguration(new CurrencyMapping());
            modelBuilder.ApplyConfiguration(new CustomCategoryMapping());
            modelBuilder.ApplyConfiguration(new CustomCategoryMapTypeMapping());
            modelBuilder.ApplyConfiguration(new CustomCategoryMapTypeOptionMapping());
            modelBuilder.ApplyConfiguration(new CustomCategoryTypeMapping());
            modelBuilder.ApplyConfiguration(new DepartmentMapping());
            modelBuilder.ApplyConfiguration(new DocumentFileStoreMapping());
            modelBuilder.ApplyConfiguration(new DocumentMetadataMapping());
            modelBuilder.ApplyConfiguration(new EmailAttachmentMapping());
            modelBuilder.ApplyConfiguration(new EmailJobQueueMapping());
            modelBuilder.ApplyConfiguration(new EmailQueueMapping());
            modelBuilder.ApplyConfiguration(new EmailRecipientMapping());
            modelBuilder.ApplyConfiguration(new EmailRecipientToMapping());
            modelBuilder.ApplyConfiguration(new EmailRecipientCcMapping());
            modelBuilder.ApplyConfiguration(new EmailRecipientBccMapping());
            modelBuilder.ApplyConfiguration(new EmailTemplateMapping());
            modelBuilder.ApplyConfiguration(new EmployeeMapping());
            modelBuilder.ApplyConfiguration(new EmployeeCostCentreMapping());
            modelBuilder.ApplyConfiguration(new EmployeeSiteMapping());
            modelBuilder.ApplyConfiguration(new EntityContactMapping());
            modelBuilder.ApplyConfiguration(new EntityLayoutTemplateMapping());
            modelBuilder.ApplyConfiguration(new EntityTypeMapping());
            modelBuilder.ApplyConfiguration(new ErrorLogMapping());
            modelBuilder.ApplyConfiguration(new FileStoreSettingMapping());
            modelBuilder.ApplyConfiguration(new PhotoMapping());
            modelBuilder.ApplyConfiguration(new PostalCodeMapping());
            modelBuilder.ApplyConfiguration(new PublicHolidayMapping());
            modelBuilder.ApplyConfiguration(new RecordInfoMapping());
            modelBuilder.ApplyConfiguration(new RegionMapping());
            modelBuilder.ApplyConfiguration(new SecurityConfigurationMapping());
            modelBuilder.ApplyConfiguration(new SecurityProfileMapping());
            modelBuilder.ApplyConfiguration(new EmailServerMapping());
            modelBuilder.ApplyConfiguration(new StateMapping());
            modelBuilder.ApplyConfiguration(new SystemEntityMapping());
            modelBuilder.ApplyConfiguration(new SystemEntityRightDependencyMapping());
            modelBuilder.ApplyConfiguration(new SystemEntityRightMapping());
            modelBuilder.ApplyConfiguration(new SystemModuleMapping());
            modelBuilder.ApplyConfiguration(new TimeZoneMapping());
            modelBuilder.ApplyConfiguration(new UserBusinessProfileMapping());

            //modelBuilder.Entity<Address>().HasDiscriminator<int>("EntityTypeId")
            //.HasValue<BusinessProfileAddress>(Configuration.ApplicationEntityType.BusinessProfile);

            //modelBuilder.Entity<Department>().HasDiscriminator<int>("EntityTypeId")
            //.HasValue<BusinessProfileDepartment>(Configuration.ApplicationEntityType.BusinessProfile);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationUserCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUsers_Contact_CreatedBy");

            modelBuilder.Entity<ApplicationUser>().
                HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationUserLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_AspNetUsers_Contact_UpdatedBy");

            modelBuilder.Entity<ApplicationRole>().
            HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationRoleCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetRoles_Contact_CreatedBy");

            modelBuilder.Entity<ApplicationRole>().
                HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationRoleLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_AspNetRoles_Contact_UpdatedBy");

            modelBuilder.Entity<ApplicationRole>()
                .HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationRoleLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_AspNetRoles_Contact_UpdatedBy");

            modelBuilder.Entity<ApplicationRole>()
                .HasOne(d => d.BusinessProfile)
                .WithMany(p => p.ApplicationRoles)
                .HasForeignKey(d => d.BusinessProfileId)
                .HasConstraintName("FK_AspNetRoles_BusinessProfile_BusinessProfileId");

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(d => d.Contact)
                .WithOne(p => p.ApplicationUser)
                //.HasForeignKey(d => d.ContactId);
                .HasForeignKey<ApplicationUser>(d => d.ContactId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(d => d.SecurityProfile)
                .WithMany(p => p.ApplicationUsers)
                .HasForeignKey(d => d.SecurityProfileId)
                .HasConstraintName("FK_AspNetUsers_SecurityProfile_SecurityProfileId");

            modelBuilder.Entity<ApplicationRole>()
                .HasOne(d => d.ParentRole)
                .WithMany(p => p.InverseParentRole)
                .HasForeignKey(d => d.ParentRoleId)
                .HasConstraintName("FK_AspNetRoles_AspNetRoles_ParentId");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ItmConfigurations.IdentityServerConnectionString);
            //optionsBuilder.UseSqlServer(@"data source=.;initial catalog=TestDb;user id=sa;password=Test123;", b => b.MigrationsAssembly("Itm.Web.Api"));
            //optionsBuilder.UseSqlServer(ItmConfigurations.IdentityServerConnectionString, b => b.MigrationsAssembly("Itm.Web.Api"));
        }
    }
}
