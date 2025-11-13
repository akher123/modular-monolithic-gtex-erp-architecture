using System;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApploicationService.Models
{
    public class BusinessProfileModel
    {
        public BusinessProfileModel()
        {
            Logo = new PhotoModel();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Company Code is required")]
        [MaxLength(3, ErrorMessage = "Maximum length of Company Code is 3 characters.")]
        public string CompId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Maximum length of name is 100 characters.")]
        public string CompanyName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of number is 50 characters.")]
        public string Number { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of ABN is 20 characters.")]
        public string Abn { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of ACN is 20 characters.")]
        public string Acn { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of phone is 20 characters.")]
        public string Phone { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of secondary phone is 20 characters.")]
        public string SecondaryPhone { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of mobile is 20 characters.")]
        public string Mobile { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of fax is 20 characters.")]
        public string Fax { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of email is 100 characters.")]
        public string Email { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of secondary email is 100 characters.")]
        public string SecondaryEmail { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of website is 200 characters.")]
        public string Website { get; set; }
        public bool UseRegion { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of Domain Name is 20 characters.")]
        public string DomainName { get; set; }
        public int? LogoId { get; set; }
        public int? ReceiptHeaderAddressId { get; set; }
        public int? DefaultFinancialOptionId { get; set; }
        public int? PostalAddressId { get; set; }
        public int? DeliveryAddressId { get; set; }
        public bool EnableMultipleOperatingCountries { get; set; }
        public bool EnableMultipleOperatingStates { get; set; }
        public byte[] SchedulerObject { get; set; }
        public byte[] AccountingIntegrationConfiguration { get; set; }
        public byte[] PayrollIntegrationConfiguration { get; set; }
        public bool UseSameConfigForPayrollIntegration { get; set; }
        public bool DisableViewCustomisation { get; set; }
        public bool DisableConcurrencyWarning { get; set; }
        public bool DisableUnsavedPrompt { get; set; }
        public bool AllowAccessHours { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RecordInfoId { get; set; }
        public string TimeZoneId { get; set; }
        public bool DisableEmail { get; set; }
        public string ApplicationUrl { get; set; }
        public Guid UniqueEntityId { get; set; }
        public PhotoModel Logo { get; set; }
    }
}
