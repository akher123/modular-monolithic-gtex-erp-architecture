using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessProfile : ITrackable, IUniqueEntity
    {
        public BusinessProfile()
        {
            BusinessProfileAddresses = new HashSet<BusinessProfileAddress>();
            Communications = new HashSet<Communication>();
            Companies = new HashSet<Company>();
            CustomCategories = new HashSet<CustomCategory>();
            DocumentMetadatas = new HashSet<DocumentMetadata>();
            EmailJobQueues = new HashSet<EmailJobQueue>();
            EntityLayoutTemplates = new HashSet<EntityLayoutTemplate>();
            ErrorLogs = new HashSet<ErrorLog>();
            FileStoreSettings = new HashSet<FileStoreSetting>();
            SecurityConfigurations = new HashSet<SecurityConfiguration>();
            SecurityProfiles = new HashSet<SecurityProfile>();
            ApplicationRoles = new HashSet<ApplicationRole>();
            ContactBusinessProfiles = new HashSet<ContactBusinessProfile>();
            Servers = new HashSet<EmailServer>();
            BusinessUnits = new HashSet<BusinessUnit>();
            CostCentres = new HashSet<CostCentre>();
            BusinessProfileDepartments = new HashSet<BusinessProfileDepartment>();
            BusinessProfileSites = new HashSet<BusinessProfileSite>();

            UserBusinessProfiles = new HashSet<UserBusinessProfile>();


        }

        public int Id { get; set; }
        public string CompId { get; set; }
        public string CompanyName { get; set; }
        public string Number { get; set; }
        public string Abn { get; set; }
        public string Acn { get; set; }
        public string Phone { get; set; }
        public string SecondaryPhone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SecondaryEmail { get; set; }
        public string Website { get; set; }
        public bool UseRegion { get; set; }
        public string DomainName { get; set; }
        public int? LogoId { get; set; }
        public bool EnableMultipleOperatingCountries { get; set; }
        public bool EnableMultipleOperatingStates { get; set; }
        public byte[] SchedulerObject { get; set; }
        public byte[] AccountingIntegrationConfiguration { get; set; }
        public byte[] PayrollIntegrationConfiguration { get; set; }
        public bool? UseSameConfigForPayrollIntegration { get; set; }
        public bool DisableViewCustomisation { get; set; }
        public bool DisableConcurrencyWarning { get; set; }
        public bool DisableUnsavedPrompt { get; set; }
        public bool AllowAccessHours { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public string TimeZoneId { get; set; }
        public bool DisableEmail { get; set; }
        public string ApplicationUrl { get; set; }
        public Guid UniqueEntityId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Logo { get; set; }

        public RecordInfo RecordInfo { get; set; }

        public ICollection<BusinessProfileAddress> BusinessProfileAddresses { get; set; }
        public ICollection<Communication> Communications { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<CustomCategory> CustomCategories { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadatas { get; set; }
        public ICollection<EmailJobQueue> EmailJobQueues { get; set; }
        public ICollection<EntityLayoutTemplate> EntityLayoutTemplates { get; set; }
        public ICollection<ErrorLog> ErrorLogs { get; set; }
        public ICollection<FileStoreSetting> FileStoreSettings { get; set; }
        public ICollection<SecurityConfiguration> SecurityConfigurations { get; set; }
        public ICollection<SecurityProfile> SecurityProfiles { get; set; }
        public ICollection<ApplicationRole> ApplicationRoles { get; set; }
        public ICollection<ContactBusinessProfile> ContactBusinessProfiles { get; set; }
        public ICollection<EmailServer> Servers { get; set; }

        public ICollection<BusinessUnit> BusinessUnits { get; set; }
        public ICollection<CostCentre> CostCentres { get; set; }
        public ICollection<BusinessProfileDepartment> BusinessProfileDepartments { get; set; }

        public ICollection<BusinessProfileSite> BusinessProfileSites { get; set; }

        public ICollection<UserBusinessProfile> UserBusinessProfiles { get; set; }


        [NotMapped]
        public int EntityTypeId { get; set; } = Configuration.ApplicationEntityType.BusinessProfile;


    }
}
