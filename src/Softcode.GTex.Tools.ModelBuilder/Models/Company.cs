using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyOperatingState = new HashSet<CompanyOperatingState>();
            CompanyRelationshipType = new HashSet<CompanyRelationshipType>();
            EntityContact = new HashSet<EntityContact>();
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
        public int? RegionId { get; set; }
        public bool? IsActive { get; set; }
        public int? IndustryTypeId { get; set; }
        public int? CompanyTypeId { get; set; }
        public int? RatingTypeId { get; set; }
        public int? NoOfEmployee { get; set; }
        public int? PrimaryContactId { get; set; }
        public decimal? AnnualTurnover { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsIndividual { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public int? PreferredContactMethodId { get; set; }
        public int? MembershipId { get; set; }
        public string TimeZoneId { get; set; }
        public string ExternalPartnerId { get; set; }
        public byte[] TimeStamp { get; set; }
        public int BusinessProfileId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public CustomCategory CompanyType { get; set; }
        public Contact CreatedByContact { get; set; }
        public CustomCategory IndustryType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Logo { get; set; }
        public CustomCategory PreferredContactMethod { get; set; }
        public Contact PrimaryContact { get; set; }
        public CustomCategory RatingType { get; set; }
        public Region Region { get; set; }
        public ICollection<CompanyOperatingState> CompanyOperatingState { get; set; }
        public ICollection<CompanyRelationshipType> CompanyRelationshipType { get; set; }
        public ICollection<EntityContact> EntityContact { get; set; }
    }
}
