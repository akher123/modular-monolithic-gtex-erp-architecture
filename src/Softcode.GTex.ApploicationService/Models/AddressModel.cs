using System;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApploicationService.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Street is required")]
        [MaxLength(100, ErrorMessage = "Maximum length of street is 100 characters.")]
        public string Street { get; set; }        
        [MaxLength(50, ErrorMessage = "Maximum length of suburb/town is 50 characters.")]
        public string Suburb { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        [MaxLength(10, ErrorMessage = "Maximum length of postcode is 10 characters.")]
        public string PostCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required")]
        public int CountryId { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid UniqueEntityId { get; set; }
        public string DisplayAddress { get; set; }
        public bool IsDirty { get; set; }
        public virtual int EntityId { get; set; }

    }
}
