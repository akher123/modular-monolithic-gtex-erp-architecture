using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService.Models
{
    public class ContactModel
    {
        public ContactModel()
        {
            this.Photo = new PhotoModel();
            this.ContactSpecialisationIds = new List<int>();
            this.CompanyIds = new List<int>();
            this.BusinessProfileIds = new List<int>();
            this.ContactAddresses= new List<ContactAddressModel>();
        }

        public int Id { get; set; }
        public int ContactType { get; set; }
        public int? TitleId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length of first name is 50 characters.")]
        public string FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of middle name is 50 characters.")]
        public string MiddleName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length of last name is 50 characters.")]
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime? DateOfBirthDateFormat { get; set; }
        public int? PostionId { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of business phone is 20 characters.")]
        public string BusinessPhone { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of business phone ext is 20 characters.")]
        public string BusinessPhoneExt { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of home phone is 20 characters.")]
        public string HomePhone { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of mobile phone is 20 characters.")]
        public string Mobile { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum length of fax phone is 20 characters.")]
        public string Fax { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of email is 100 characters.")]
        public string Email { get; set; }
        public int? ImTypeId { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of IM Login Id is 50 characters.")]
        public string ImLoginId { get; set; }
        public int? ImTypeId2 { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of additional IM Login Id is 50 characters.")]
        public string ImLoginId3 { get; set; }
        public int? ImTypeId3 { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of additional IM Login Id 2 is 50 characters.")]
        public string ImLoginId2 { get; set; }
        public int? PhotoId { get; set; }
        public bool IsActive { get; set; } = true;        
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public int? PreferredContactMethodId { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of website is 200 characters.")]
        public string Website { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of preferred name is 50 characters.")]
        public string PreferredName { get; set; }
        public int? PreferredPhoneType { get; set; }
        public int? IdentityLicenseId { get; set; }
        public bool IsBusinessEntity { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of TimeZone Id is 200 characters.")]
        public string TimeZoneId { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of additional email is 100 characters.")]
        public string Email2 { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of additional email 2 is 100 characters.")]
        public string Email3 { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of emergency contact name is 100 characters.")]
        public string EmergencyContactName { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of emergency contact relation is 200 characters.")]
        public string EmergencyContactRelation { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length of emergency contact number is 50 characters.")]
        public string EmergencyContactNumber { get; set; }
        [MaxLength(400, ErrorMessage = "Maximum length of special instruction is 400 characters.")]
        public string SpecialInstruction { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of emergency contact email is 100 characters.")]
        public string EmergencyContactEmail { get; set; }
        public bool? IsPrimaryContact { get; set; }
        public PhotoModel Photo { get; set; }
        public List<int> ContactSpecialisationIds { get; set; }
        public List<int> BusinessProfileIds { get; set; }
        public List<int> CompanyIds { get; set; }
        public int BusinessProfileId { get; set; }
        public int CompanyId { get; set; }
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }
        public Guid UniqueEntityId { get; set; }

        public List<ContactAddressModel> ContactAddresses { get; set; }

    }
}
