using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Contact
    {
        public Contact()
        {
            AccessLogCreatedByContact = new HashSet<AccessLog>();
            AccessLogLastUpdatedByContact = new HashSet<AccessLog>();
            ApplicationMenuCreatedByContact = new HashSet<ApplicationMenu>();
            ApplicationMenuLastUpdatedByContact = new HashSet<ApplicationMenu>();
            ApplicationPageCreatedByContact = new HashSet<ApplicationPage>();
            ApplicationPageFieldDetailCreatedByContact = new HashSet<ApplicationPageFieldDetail>();
            ApplicationPageFieldDetailLastUpdatedByContact = new HashSet<ApplicationPageFieldDetail>();
            ApplicationPageLastUpdatedByContact = new HashSet<ApplicationPage>();
            ApplicationPageNavigationCreatedByContact = new HashSet<ApplicationPageNavigation>();
            ApplicationPageNavigationLastUpdatedByContact = new HashSet<ApplicationPageNavigation>();
            ApplicationPageServiceCreatedByContact = new HashSet<ApplicationPageService>();
            ApplicationPageServiceLastUpdatedByContact = new HashSet<ApplicationPageService>();
            AspNetRolesCreatedByContact = new HashSet<AspNetRoles>();
            AspNetRolesLastUpdatedByContact = new HashSet<AspNetRoles>();
            AspNetUsersContact = new HashSet<AspNetUsers>();
            AspNetUsersCreatedByContact = new HashSet<AspNetUsers>();
            AspNetUsersLastUpdatedByContact = new HashSet<AspNetUsers>();
            BusinessProfileCreatedByContact = new HashSet<BusinessProfile>();
            BusinessProfileLastUpdatedByContact = new HashSet<BusinessProfile>();
            CommunicationCreatedByContact = new HashSet<Communication>();
            CommunicationFollowupByContact = new HashSet<Communication>();
            CommunicationLastUpdatedByContact = new HashSet<Communication>();
            CompanyCreatedByContact = new HashSet<Company>();
            CompanyLastUpdatedByContact = new HashSet<Company>();
            CompanyPrimaryContact = new HashSet<Company>();
            ContactBusinessProfile = new HashSet<ContactBusinessProfile>();
            ContactOperatingCity = new HashSet<ContactOperatingCity>();
            ContactSpecialisation = new HashSet<ContactSpecialisation>();
            CountryCreatedByContact = new HashSet<Country>();
            CountryLastUpdatedByContact = new HashSet<Country>();
            CurrencyCreatedByContact = new HashSet<Currency>();
            CurrencyLastUpdatedByContact = new HashSet<Currency>();
            CustomCategoryCreatedByContact = new HashSet<CustomCategory>();
            CustomCategoryLastUpdatedByContact = new HashSet<CustomCategory>();
            DocumentFileStoreCreatedByContact = new HashSet<DocumentFileStore>();
            DocumentFileStoreLastUpdatedByContact = new HashSet<DocumentFileStore>();
            DocumentMetadataCreatedByContact = new HashSet<DocumentMetadata>();
            DocumentMetadataLastUpdatedByContact = new HashSet<DocumentMetadata>();
            EmployeeContact = new HashSet<Employee>();
            EmployeeCreatedByContact = new HashSet<Employee>();
            EmployeeLastUpdatedByContact = new HashSet<Employee>();
            EntityContact = new HashSet<EntityContact>();
            EntityLayoutTemplateCreatedByContact = new HashSet<EntityLayoutTemplate>();
            EntityLayoutTemplateLastUpdatedByContact = new HashSet<EntityLayoutTemplate>();
            FileStoreSettingCreatedByContact = new HashSet<FileStoreSetting>();
            FileStoreSettingLastUpdatedByContact = new HashSet<FileStoreSetting>();
            InverseCreatedByContact = new HashSet<Contact>();
            InverseLastUpdatedByContact = new HashSet<Contact>();
            PhotoCreatedByContact = new HashSet<Photo>();
            PhotoLastUpdatedByContact = new HashSet<Photo>();
            PostalCodeCreatedByContact = new HashSet<PostalCode>();
            PostalCodeLastUpdatedByContact = new HashSet<PostalCode>();
            PublicHolidayCreatedByContact = new HashSet<PublicHoliday>();
            PublicHolidayLastUpdatedByContact = new HashSet<PublicHoliday>();
            RegionCreatedByContact = new HashSet<Region>();
            RegionLastUpdatedByContact = new HashSet<Region>();
            SecurityConfigurationCreatedByContact = new HashSet<SecurityConfiguration>();
            SecurityConfigurationLastUpdatedByContact = new HashSet<SecurityConfiguration>();
            SecurityProfileCreatedByContact = new HashSet<SecurityProfile>();
            SecurityProfileLastUpdatedByContact = new HashSet<SecurityProfile>();
            ServerCreatedByContact = new HashSet<Server>();
            ServerLastUpdatedByContact = new HashSet<Server>();
            StateCreatedByContact = new HashSet<State>();
            StateLastUpdatedByContact = new HashSet<State>();
            VoucherTypeCreatedByContact = new HashSet<VoucherType>();
            VoucherTypeLastUpdatedByContact = new HashSet<VoucherType>();
        }

        public int Id { get; set; }
        public int ContactType { get; set; }
        public int? TitleId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? PostionId { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessPhoneExt { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? ImTypeId { get; set; }
        public string ImLoginId { get; set; }
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
        public string EmergencyContactEmail { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string SpecialInstruction { get; set; }
        public bool? IsPrimaryContact { get; set; }

        public Contact CreatedByContact { get; set; }
        public CustomCategory Gender { get; set; }
        public CustomCategory IdentityLicense { get; set; }
        public CustomCategory ImType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Photo { get; set; }
        public CustomCategory Postion { get; set; }
        public TimeZone TimeZone { get; set; }
        public ICollection<AccessLog> AccessLogCreatedByContact { get; set; }
        public ICollection<AccessLog> AccessLogLastUpdatedByContact { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenuCreatedByContact { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenuLastUpdatedByContact { get; set; }
        public ICollection<ApplicationPage> ApplicationPageCreatedByContact { get; set; }
        public ICollection<ApplicationPageFieldDetail> ApplicationPageFieldDetailCreatedByContact { get; set; }
        public ICollection<ApplicationPageFieldDetail> ApplicationPageFieldDetailLastUpdatedByContact { get; set; }
        public ICollection<ApplicationPage> ApplicationPageLastUpdatedByContact { get; set; }
        public ICollection<ApplicationPageNavigation> ApplicationPageNavigationCreatedByContact { get; set; }
        public ICollection<ApplicationPageNavigation> ApplicationPageNavigationLastUpdatedByContact { get; set; }
        public ICollection<ApplicationPageService> ApplicationPageServiceCreatedByContact { get; set; }
        public ICollection<ApplicationPageService> ApplicationPageServiceLastUpdatedByContact { get; set; }
        public ICollection<AspNetRoles> AspNetRolesCreatedByContact { get; set; }
        public ICollection<AspNetRoles> AspNetRolesLastUpdatedByContact { get; set; }
        public ICollection<AspNetUsers> AspNetUsersContact { get; set; }
        public ICollection<AspNetUsers> AspNetUsersCreatedByContact { get; set; }
        public ICollection<AspNetUsers> AspNetUsersLastUpdatedByContact { get; set; }
        public ICollection<BusinessProfile> BusinessProfileCreatedByContact { get; set; }
        public ICollection<BusinessProfile> BusinessProfileLastUpdatedByContact { get; set; }
        public ICollection<Communication> CommunicationCreatedByContact { get; set; }
        public ICollection<Communication> CommunicationFollowupByContact { get; set; }
        public ICollection<Communication> CommunicationLastUpdatedByContact { get; set; }
        public ICollection<Company> CompanyCreatedByContact { get; set; }
        public ICollection<Company> CompanyLastUpdatedByContact { get; set; }
        public ICollection<Company> CompanyPrimaryContact { get; set; }
        public ICollection<ContactBusinessProfile> ContactBusinessProfile { get; set; }
        public ICollection<ContactOperatingCity> ContactOperatingCity { get; set; }
        public ICollection<ContactSpecialisation> ContactSpecialisation { get; set; }
        public ICollection<Country> CountryCreatedByContact { get; set; }
        public ICollection<Country> CountryLastUpdatedByContact { get; set; }
        public ICollection<Currency> CurrencyCreatedByContact { get; set; }
        public ICollection<Currency> CurrencyLastUpdatedByContact { get; set; }
        public ICollection<CustomCategory> CustomCategoryCreatedByContact { get; set; }
        public ICollection<CustomCategory> CustomCategoryLastUpdatedByContact { get; set; }
        public ICollection<DocumentFileStore> DocumentFileStoreCreatedByContact { get; set; }
        public ICollection<DocumentFileStore> DocumentFileStoreLastUpdatedByContact { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadataCreatedByContact { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadataLastUpdatedByContact { get; set; }
        public ICollection<Employee> EmployeeContact { get; set; }
        public ICollection<Employee> EmployeeCreatedByContact { get; set; }
        public ICollection<Employee> EmployeeLastUpdatedByContact { get; set; }
        public ICollection<EntityContact> EntityContact { get; set; }
        public ICollection<EntityLayoutTemplate> EntityLayoutTemplateCreatedByContact { get; set; }
        public ICollection<EntityLayoutTemplate> EntityLayoutTemplateLastUpdatedByContact { get; set; }
        public ICollection<FileStoreSetting> FileStoreSettingCreatedByContact { get; set; }
        public ICollection<FileStoreSetting> FileStoreSettingLastUpdatedByContact { get; set; }
        public ICollection<Contact> InverseCreatedByContact { get; set; }
        public ICollection<Contact> InverseLastUpdatedByContact { get; set; }
        public ICollection<Photo> PhotoCreatedByContact { get; set; }
        public ICollection<Photo> PhotoLastUpdatedByContact { get; set; }
        public ICollection<PostalCode> PostalCodeCreatedByContact { get; set; }
        public ICollection<PostalCode> PostalCodeLastUpdatedByContact { get; set; }
        public ICollection<PublicHoliday> PublicHolidayCreatedByContact { get; set; }
        public ICollection<PublicHoliday> PublicHolidayLastUpdatedByContact { get; set; }
        public ICollection<Region> RegionCreatedByContact { get; set; }
        public ICollection<Region> RegionLastUpdatedByContact { get; set; }
        public ICollection<SecurityConfiguration> SecurityConfigurationCreatedByContact { get; set; }
        public ICollection<SecurityConfiguration> SecurityConfigurationLastUpdatedByContact { get; set; }
        public ICollection<SecurityProfile> SecurityProfileCreatedByContact { get; set; }
        public ICollection<SecurityProfile> SecurityProfileLastUpdatedByContact { get; set; }
        public ICollection<Server> ServerCreatedByContact { get; set; }
        public ICollection<Server> ServerLastUpdatedByContact { get; set; }
        public ICollection<State> StateCreatedByContact { get; set; }
        public ICollection<State> StateLastUpdatedByContact { get; set; }
        public ICollection<VoucherType> VoucherTypeCreatedByContact { get; set; }
        public ICollection<VoucherType> VoucherTypeLastUpdatedByContact { get; set; }
    }
}
