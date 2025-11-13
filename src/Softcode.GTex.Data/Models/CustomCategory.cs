using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CustomCategory: ITrackable
    {
        public CustomCategory()
        {
            CommunicationMethodTypes = new HashSet<Communication>();
            CommunicationStatus = new HashSet<Communication>();
            ContactIdentityLicenses = new HashSet<Contact>();
            ContactPostions = new HashSet<Contact>();
            CompanyCompanyTypes = new HashSet<Company>();
            CompanyIndustryTypes = new HashSet<Company>();
            CompanyPreferredContactMethods = new HashSet<Company>();
            CompanyRatingTypes = new HashSet<Company>();
            DocumentMetadatas = new HashSet<DocumentMetadata>();
            ContactSpecialisations = new HashSet<ContactSpecialisation>();
            ContactTitles = new HashSet<Contact>();
            Addresses = new HashSet<Address>();
            Employees = new HashSet<Employee>();

            ContactImTypes = new HashSet<Contact>();
            ContactImTypes2 = new HashSet<Contact>();
            ContactImTypes3 = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public int CustomCategoryTypeId { get; set; }
        public int? CustomCategoryMapTypeOptionId { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public int? BusinessProfileId { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public CustomCategoryMapTypeOption CustomCategoryMapTypeOption { get; set; }
        public CustomCategoryType CustomCategoryType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<Communication> CommunicationMethodTypes { get; set; }
        public ICollection<Communication> CommunicationStatus { get; set; }
        public ICollection<Contact> ContactIdentityLicenses { get; set; }
        public ICollection<Contact> ContactPostions { get; set; }
        public ICollection<Contact> ContactGenders { get; set; }
        public ICollection<Contact> ContactImTypes { get; set; }
        public ICollection<Contact> ContactImTypes2 { get; set; }
        public ICollection<Contact> ContactImTypes3 { get; set; }
        public ICollection<Contact> ContactPreferredContactMethods { get; set; }
        public ICollection<Company> CompanyCompanyTypes { get; set; }
        public ICollection<Company> CompanyIndustryTypes { get; set; }
        public ICollection<Company> CompanyPreferredContactMethods { get; set; }
        public ICollection<Company> CompanyRatingTypes { get; set; }
        public ICollection<ContactSpecialisation> ContactSpecialisations { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadatas { get; set; }
        public ICollection<Contact> ContactTitles { get; set; }
        public ICollection<Address> Addresses { get; set; }

        public ICollection<Employee> Employees { get; set; }


    }
}
