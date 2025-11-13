using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    
    public class BusinessProfileAddress :Address
    {
        //public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        
        public BusinessProfile BusinessProfile { get; set; }

        [NotMapped]
        public override int EntityId { get { return BusinessProfileId; } set { BusinessProfileId = value; } }

        //public Address Address { get; set; }
    }
}
