using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessProfile
    {
        public BusinessProfile()
        {
            AspNetRoles = new HashSet<AspNetRoles>();
            Communication = new HashSet<Communication>();
            Company = new HashSet<Company>();
            ContactBusinessProfile = new HashSet<ContactBusinessProfile>();
            CustomCategory = new HashSet<CustomCategory>();
            DocumentMetadata = new HashSet<DocumentMetadata>();
            EntityLayoutTemplate = new HashSet<EntityLayoutTemplate>();
            ErrorLog = new HashSet<ErrorLog>();
            FileStoreSetting = new HashSet<FileStoreSetting>();
            SecurityConfiguration = new HashSet<SecurityConfiguration>();
            SecurityProfile = new HashSet<SecurityProfile>();
        }

        public int Id { get; set; }
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
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Logo { get; set; }
        public ICollection<AspNetRoles> AspNetRoles { get; set; }
        public ICollection<Communication> Communication { get; set; }
        public ICollection<Company> Company { get; set; }
        public ICollection<ContactBusinessProfile> ContactBusinessProfile { get; set; }
        public ICollection<CustomCategory> CustomCategory { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadata { get; set; }
        public ICollection<EntityLayoutTemplate> EntityLayoutTemplate { get; set; }
        public ICollection<ErrorLog> ErrorLog { get; set; }
        public ICollection<FileStoreSetting> FileStoreSetting { get; set; }
        public ICollection<SecurityConfiguration> SecurityConfiguration { get; set; }
        public ICollection<SecurityProfile> SecurityProfile { get; set; }
    }
}
