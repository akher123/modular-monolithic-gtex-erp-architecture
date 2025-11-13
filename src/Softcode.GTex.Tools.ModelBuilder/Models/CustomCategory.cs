using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CustomCategory
    {
        public CustomCategory()
        {
            CommunicationMethodType = new HashSet<Communication>();
            CommunicationStatus = new HashSet<Communication>();
            CompanyCompanyType = new HashSet<Company>();
            CompanyIndustryType = new HashSet<Company>();
            CompanyPreferredContactMethod = new HashSet<Company>();
            CompanyRatingType = new HashSet<Company>();
            CompanyRelationshipType = new HashSet<CompanyRelationshipType>();
            ContactGender = new HashSet<Contact>();
            ContactIdentityLicense = new HashSet<Contact>();
            ContactImType = new HashSet<Contact>();
            ContactPostion = new HashSet<Contact>();
            ContactSpecialisation = new HashSet<ContactSpecialisation>();
            DocumentMetadata = new HashSet<DocumentMetadata>();
            EmployeeBloodGroup = new HashSet<Employee>();
            EmployeeEmpStatus = new HashSet<Employee>();
            EmployeeNationality = new HashSet<Employee>();
            EmployeeReligion = new HashSet<Employee>();
            EmployeeTerminationType = new HashSet<Employee>();
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
        public ICollection<Communication> CommunicationMethodType { get; set; }
        public ICollection<Communication> CommunicationStatus { get; set; }
        public ICollection<Company> CompanyCompanyType { get; set; }
        public ICollection<Company> CompanyIndustryType { get; set; }
        public ICollection<Company> CompanyPreferredContactMethod { get; set; }
        public ICollection<Company> CompanyRatingType { get; set; }
        public ICollection<CompanyRelationshipType> CompanyRelationshipType { get; set; }
        public ICollection<Contact> ContactGender { get; set; }
        public ICollection<Contact> ContactIdentityLicense { get; set; }
        public ICollection<Contact> ContactImType { get; set; }
        public ICollection<Contact> ContactPostion { get; set; }
        public ICollection<ContactSpecialisation> ContactSpecialisation { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadata { get; set; }
        public ICollection<Employee> EmployeeBloodGroup { get; set; }
        public ICollection<Employee> EmployeeEmpStatus { get; set; }
        public ICollection<Employee> EmployeeNationality { get; set; }
        public ICollection<Employee> EmployeeReligion { get; set; }
        public ICollection<Employee> EmployeeTerminationType { get; set; }
    }
}
