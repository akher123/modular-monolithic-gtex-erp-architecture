using System;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApploicationService.Models
{
    public class ContactAddressModel : AddressModel
    {
        //public int Id { get; set; }
        public int ContactId { get; set; }

        //public override int EntityId { get { return ContactId; } set { ContactId = value; } }

    }
}
