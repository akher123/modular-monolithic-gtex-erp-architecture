using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ContactSpecialisation
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int SpecialisationId { get; set; }
        public string OtherSpecialisation { get; set; }

        public Contact Contact { get; set; }
        public CustomCategory Specialisation { get; set; }
    }
}
