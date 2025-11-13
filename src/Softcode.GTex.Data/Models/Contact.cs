using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Softcode.GTex.Data.Models
{
    public partial class Contact : ITrackable, IUniqueEntity
    {
        public Contact()
        {
            AccessLogCreatedByContacts = new HashSet<AccessLog>();
            AccessLogLastUpdatedByContacts = new HashSet<AccessLog>();
            ApplicationMenuCreatedByContacts = new HashSet<ApplicationMenu>();
            ApplicationMenuLastUpdatedByContacts = new HashSet<ApplicationMenu>();
            ApplicationPageCreatedByContacts = new HashSet<ApplicationPage>();
            ApplicationPageFieldDetailCreatedByContacts = new HashSet<ApplicationPageListField>();
            ApplicationPageFieldDetailLastUpdatedByContacts = new HashSet<ApplicationPageListField>();
            ApplicationPageLastUpdatedByContacts = new HashSet<ApplicationPage>();
            ApplicationPageNavigationCreatedByContacts = new HashSet<ApplicationPageNavigation>();
            ApplicationPageNavigationLastUpdatedByContacts = new HashSet<ApplicationPageNavigation>();
            ApplicationPageServiceCreatedByContacts = new HashSet<ApplicationPageService>();
            ApplicationPageServiceLastUpdatedByContacts = new HashSet<ApplicationPageService>();
            ApplicationRoleCreatedByContacts = new HashSet<ApplicationRole>();
            ApplicationRoleLastUpdatedByContacts = new HashSet<ApplicationRole>();
            //ApplicationUserUserContacts = new HashSet<ApplicationUser>();
            ApplicationUserCreatedByContacts = new HashSet<ApplicationUser>();
            ApplicationUserLastUpdatedByContacts = new HashSet<ApplicationUser>();
            BusinessProfileCreatedByContacts = new HashSet<BusinessProfile>();
            BusinessProfileLastUpdatedByContacts = new HashSet<BusinessProfile>();
            CommunicationCreatedByContacts = new HashSet<Communication>();
            CommunicationFollowupByContacts = new HashSet<Communication>();
            CommunicationLastUpdatedByContacts = new HashSet<Communication>();
            CompanyCreatedByContacts = new HashSet<Company>();
            CompanyLastUpdatedByContacts = new HashSet<Company>();
            CompanyPrimaryContacts = new HashSet<Company>();
            ContactOperatingCities = new HashSet<ContactOperatingCity>();
            ContactSpecialisations = new HashSet<ContactSpecialisation>();
            ContactBusinessProfiles = new HashSet<ContactBusinessProfile>();
            CountryCreatedByContacts = new HashSet<Country>();
            CountryLastUpdatedByContacts = new HashSet<Country>();
            CurrencyCreatedByContacts = new HashSet<Currency>();
            CurrencyLastUpdatedByContacts = new HashSet<Currency>();
            CustomCategoryCreatedByContacts = new HashSet<CustomCategory>();
            CustomCategoryLastUpdatedByContacts = new HashSet<CustomCategory>();
            DocumentFileStoreCreatedByContacts = new HashSet<DocumentFileStore>();
            DocumentFileStoreLastUpdatedByContacts = new HashSet<DocumentFileStore>();
            DocumentMetadataCreatedByContacts = new HashSet<DocumentMetadata>();
            DocumentMetadataLastUpdatedByContacts = new HashSet<DocumentMetadata>();
            EmailJobQueueCreatedByContacts = new HashSet<EmailJobQueue>();
            EmailJobQueueLastUpdatedByContacts = new HashSet<EmailJobQueue>();
            EmailTemplateCreatedByContacts = new HashSet<EmailTemplate>();
            EmailTemplateLastUpdatedByContacts = new HashSet<EmailTemplate>();
            EntityContacts = new HashSet<EntityContact>();
            EntityLayoutTemplateCreatedByContacts = new HashSet<EntityLayoutTemplate>();
            EntityLayoutTemplateLastUpdatedByContacts = new HashSet<EntityLayoutTemplate>();
            FileStoreSettingCreatedByContacts = new HashSet<FileStoreSetting>();
            FileStoreSettingLastUpdatedByContacts = new HashSet<FileStoreSetting>();
            InverseCreatedByContact = new HashSet<Contact>();
            InverseLastUpdatedByContact = new HashSet<Contact>();
            PhotoCreatedByContacts = new HashSet<Photo>();
            PhotoLastUpdatedByContacts = new HashSet<Photo>();
            PostalCodeCreatedByContacts = new HashSet<PostalCode>();
            PostalCodeLastUpdatedByContacts = new HashSet<PostalCode>();
            PublicHolidayCreatedByContacts = new HashSet<PublicHoliday>();
            PublicHolidayLastUpdatedByContacts = new HashSet<PublicHoliday>();
            RegionCreatedByContacts = new HashSet<Region>();
            RegionLastUpdatedByContacts = new HashSet<Region>();
            SecurityConfigurationCreatedByContacts = new HashSet<SecurityConfiguration>();
            SecurityConfigurationLastUpdatedByContacts = new HashSet<SecurityConfiguration>();
            SecurityProfileCreatedByContacts = new HashSet<SecurityProfile>();
            SecurityProfileLastUpdatedByContacts = new HashSet<SecurityProfile>();
            ServerCreatedByContacts = new HashSet<EmailServer>();
            ServerLastUpdatedByContacts = new HashSet<EmailServer>();
            StateCreatedByContacts = new HashSet<State>();
            StateLastUpdatedByContacts = new HashSet<State>();
            AddressCreatedByContacts = new HashSet<Address>();
            AddressLastUpdatedByContacts = new HashSet<Address>();

            DepartmentCreatedByContacts = new HashSet<Department>();
            DepartmentLastUpdatedByContacts = new HashSet<Department>();

            BusinessUnitCreatedByContacts = new HashSet<BusinessUnit>();
            BusinessUnitLastUpdatedByContacts = new HashSet<BusinessUnit>();

            CostCentreCreatedByContacts = new HashSet<CostCentre>();
            CostCentreLastUpdatedByContacts = new HashSet<CostCentre>();

            BusinessProfileSitePrimaryContacts = new HashSet<BusinessProfileSite>();
            BusinessProfileSiteCreatedByContacts = new HashSet<BusinessProfileSite>();
            BusinessProfileSiteLastUpdatedByContacts = new HashSet<BusinessProfileSite>();

            ApplicationPageDetailFieldCreatedByContacts = new HashSet<ApplicationPageDetailField>();
            ApplicationPageDetailFieldLastUpdatedByContacts = new HashSet<ApplicationPageDetailField>();
            ApplicationPageActionCreatedByContacts = new HashSet<ApplicationPageAction>();
            ApplicationPageActionLastUpdatedByContacts = new HashSet<ApplicationPageAction>();

            ContactAddresses = new HashSet<ContactAddress>();
        }


        public int Id { get; set; }
        public int ContactType { get; set; }
        public int? TitleId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public string DateOfBirth { get; set; }
        public int? PostionId { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessPhoneExt { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? ImTypeId { get; set; }
        public string ImLoginId { get; set; }
        public int? ImTypeId2 { get; set; }
        public string ImLoginId2 { get; set; }
        public int? ImTypeId3 { get; set; }
        public string ImLoginId3 { get; set; }

        public int? PhotoId { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public int? PreferredContactMethodId { get; set; }
        public string Website { get; set; }
        public string PreferredName { get; set; }
        public int? PreferredPhoneType { get; set; }
        public int? IdentityLicenseId { get; set; }
        public bool IsBusinessEntity { get; set; }
        public string TimeZoneId { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public DateTime? LastExportedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string SpecialInstruction { get; set; }
        public string EmergencyContactEmail { get; set; }
        public bool? IsPrimaryContact { get; set; }
        public Guid UniqueEntityId { get; set; }
        public Contact CreatedByContact { get; set; }
        public CustomCategory IdentityLicense { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Photo { get; set; }
        public CustomCategory Postion { get; set; }
        public CustomCategory Gender { get; set; }
        public CustomCategory ImType { get; set; }
        public CustomCategory ImType2 { get; set; }
        public CustomCategory ImType3 { get; set; }
        public CustomCategory PreferredContactMethod { get; set; }
        public CustomCategory Title { get; set; }
        public TimeZone TimeZone { get; set; }
        public RecordInfo RecordInfo { get; set; }

        public Employee Employee { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<AccessLog> AccessLogCreatedByContacts { get; set; }
        public ICollection<AccessLog> AccessLogLastUpdatedByContacts { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenuCreatedByContacts { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenuLastUpdatedByContacts { get; set; }
        public ICollection<ApplicationPage> ApplicationPageCreatedByContacts { get; set; }
        public ICollection<ApplicationPageListField> ApplicationPageFieldDetailCreatedByContacts { get; set; }
        public ICollection<ApplicationPageListField> ApplicationPageFieldDetailLastUpdatedByContacts { get; set; }
        public ICollection<ApplicationPage> ApplicationPageLastUpdatedByContacts { get; set; }
        public ICollection<ApplicationPageNavigation> ApplicationPageNavigationCreatedByContacts { get; set; }
        public ICollection<ApplicationPageNavigation> ApplicationPageNavigationLastUpdatedByContacts { get; set; }
        public ICollection<ApplicationPageService> ApplicationPageServiceCreatedByContacts { get; set; }
        public ICollection<ApplicationPageService> ApplicationPageServiceLastUpdatedByContacts { get; set; }
        public ICollection<ApplicationRole> ApplicationRoleCreatedByContacts { get; set; }
        public ICollection<ApplicationRole> ApplicationRoleLastUpdatedByContacts { get; set; }
        
        public ICollection<ApplicationUser> ApplicationUserCreatedByContacts { get; set; }
        public ICollection<ApplicationUser> ApplicationUserLastUpdatedByContacts { get; set; }
        public ICollection<BusinessProfile> BusinessProfileCreatedByContacts { get; set; }
        public ICollection<BusinessProfile> BusinessProfileLastUpdatedByContacts { get; set; }
        public ICollection<Communication> CommunicationCreatedByContacts { get; set; }
        public ICollection<Communication> CommunicationFollowupByContacts { get; set; }
        public ICollection<Communication> CommunicationLastUpdatedByContacts { get; set; }
        public ICollection<Company> CompanyCreatedByContacts { get; set; }
        public ICollection<Company> CompanyLastUpdatedByContacts { get; set; }
        public ICollection<Company> CompanyPrimaryContacts { get; set; }
        public ICollection<ContactOperatingCity> ContactOperatingCities { get; set; }
        public ICollection<ContactSpecialisation> ContactSpecialisations { get; set; }
        public ICollection<ContactBusinessProfile> ContactBusinessProfiles { get; set; }
        public ICollection<Country> CountryCreatedByContacts { get; set; }
        public ICollection<Country> CountryLastUpdatedByContacts { get; set; }
        public ICollection<Currency> CurrencyCreatedByContacts { get; set; }
        public ICollection<Currency> CurrencyLastUpdatedByContacts { get; set; }
        public ICollection<CustomCategory> CustomCategoryCreatedByContacts { get; set; }
        public ICollection<CustomCategory> CustomCategoryLastUpdatedByContacts { get; set; }
        public ICollection<DocumentFileStore> DocumentFileStoreCreatedByContacts { get; set; }
        public ICollection<DocumentFileStore> DocumentFileStoreLastUpdatedByContacts { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadataCreatedByContacts { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadataLastUpdatedByContacts { get; set; }
        public ICollection<EmailJobQueue> EmailJobQueueCreatedByContacts { get; set; }
        public ICollection<EmailJobQueue> EmailJobQueueLastUpdatedByContacts { get; set; }
        public ICollection<EmailTemplate> EmailTemplateCreatedByContacts { get; set; }
        public ICollection<EmailTemplate> EmailTemplateLastUpdatedByContacts { get; set; }
        public ICollection<EntityContact> EntityContacts { get; set; }
        public ICollection<EntityLayoutTemplate> EntityLayoutTemplateCreatedByContacts { get; set; }
        public ICollection<EntityLayoutTemplate> EntityLayoutTemplateLastUpdatedByContacts { get; set; }
        public ICollection<FileStoreSetting> FileStoreSettingCreatedByContacts { get; set; }
        public ICollection<FileStoreSetting> FileStoreSettingLastUpdatedByContacts { get; set; }
        public ICollection<Contact> InverseCreatedByContact { get; set; }
        public ICollection<Contact> InverseLastUpdatedByContact { get; set; }
        public ICollection<Photo> PhotoCreatedByContacts { get; set; }
        public ICollection<Photo> PhotoLastUpdatedByContacts { get; set; }
        public ICollection<PostalCode> PostalCodeCreatedByContacts { get; set; }
        public ICollection<PostalCode> PostalCodeLastUpdatedByContacts { get; set; }
        public ICollection<PublicHoliday> PublicHolidayCreatedByContacts { get; set; }
        public ICollection<PublicHoliday> PublicHolidayLastUpdatedByContacts { get; set; }
        public ICollection<Region> RegionCreatedByContacts { get; set; }
        public ICollection<Region> RegionLastUpdatedByContacts { get; set; }
        public ICollection<SecurityConfiguration> SecurityConfigurationCreatedByContacts { get; set; }
        public ICollection<SecurityConfiguration> SecurityConfigurationLastUpdatedByContacts { get; set; }
        public ICollection<SecurityProfile> SecurityProfileCreatedByContacts { get; set; }
        public ICollection<SecurityProfile> SecurityProfileLastUpdatedByContacts { get; set; }
        public ICollection<EmailServer> ServerCreatedByContacts { get; set; }
        public ICollection<EmailServer> ServerLastUpdatedByContacts { get; set; }
        public ICollection<State> StateCreatedByContacts { get; set; }
        public ICollection<State> StateLastUpdatedByContacts { get; set; }
        public ICollection<Address> AddressCreatedByContacts { get; set; }
        public ICollection<Address> AddressLastUpdatedByContacts { get; set; }

        public ICollection<Department> DepartmentCreatedByContacts { get; set; }
        public ICollection<Department> DepartmentLastUpdatedByContacts { get; set; }

        public ICollection<BusinessUnit> BusinessUnitCreatedByContacts { get; set; }
        public ICollection<BusinessUnit> BusinessUnitLastUpdatedByContacts { get; set; }

        public ICollection<CostCentre> CostCentreCreatedByContacts { get; set; }
        public ICollection<CostCentre> CostCentreLastUpdatedByContacts { get; set; }


        public ICollection<BusinessProfileSite> BusinessProfileSitePrimaryContacts { get; set; }
        public ICollection<BusinessProfileSite> BusinessProfileSiteCreatedByContacts { get; set; }
        public ICollection<BusinessProfileSite> BusinessProfileSiteLastUpdatedByContacts { get; set; }

        public ICollection<ApplicationPageDetailField> ApplicationPageDetailFieldCreatedByContacts { get; set; }
        public ICollection<ApplicationPageDetailField> ApplicationPageDetailFieldLastUpdatedByContacts { get; set; }

        public ICollection<ApplicationPageAction> ApplicationPageActionCreatedByContacts { get; set; }
        public ICollection<ApplicationPageAction> ApplicationPageActionLastUpdatedByContacts { get; set; }



        public ICollection<ContactAddress> ContactAddresses { get; set; }

     
        
        [NotMapped]
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }

        [NotMapped]
        public int EntityTypeId { get; set; } = Configuration.ApplicationEntityType.Contact;
    }
}
