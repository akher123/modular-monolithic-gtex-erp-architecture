using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public partial class Company: ITrackable, IUniqueEntity
    {
        public Company()
        {
            CompanyOperatingStates = new HashSet<CompanyOperatingState>();
            CompanyRelationshipTypes = new HashSet<CompanyRelationshipType>();
            CompanyEntityContacts = new HashSet<EntityContact>();
            CompanyDepartments = new HashSet<CompanyDepartment>();
    }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public string TradeAs { get; set; }
        public int? FinancialOptionId { get; set; }
        public string Abn { get; set; }
        public string Acn { get; set; }
        public string MainPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int? LogoId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public bool? IsActive { get; set; }
        public int? IndustryTypeId { get; set; }
        public int? CompanyTypeId { get; set; }
        public int? RatingTypeId { get; set; }
        public int? NoOfEmployee { get; set; }
        public int? PrimaryContactId { get; set; }
        public decimal? AnnualTurnover { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public int? PreferredContactMethodId { get; set; }
        public string TimeZoneId { get; set; }
        public string ExternalPartnerId { get; set; }
        public byte[] TimeStamp { get; set; }
        public int BusinessProfileId { get; set; }
        public Guid UniqueEntityId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public CustomCategory CompanyType { get; set; }
        public Country Country { get; set; }
        public Contact CreatedByContact { get; set; }
        public CustomCategory IndustryType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Logo { get; set; }
        public CustomCategory PreferredContactMethod { get; set; }
        public Contact PrimaryContact { get; set; }
        public CustomCategory RatingType { get; set; }
        public State State { get; set; }
        public RecordInfo RecordInfo { get; set; }
        public ICollection<CompanyOperatingState> CompanyOperatingStates { get; set; }
        public ICollection<CompanyRelationshipType> CompanyRelationshipTypes { get; set; }
        public ICollection<EntityContact> CompanyEntityContacts { get; set; }

        public ICollection<CompanyDepartment> CompanyDepartments { get; set; }
        [NotMapped]
        public int EntityTypeId { get; set; } = Configuration.ApplicationEntityType.Company;
    }
}
