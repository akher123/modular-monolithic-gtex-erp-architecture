using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    
    public class ContactAddress : Address
    {
        //public int Id { get; set; }
        public int ContactId { get; set; }
        
        public Contact Contact { get; set; }

        [NotMapped]
        public override int EntityId { get { return ContactId; } set { ContactId = value; } }

        //public Address Address { get; set; }
    }
}
