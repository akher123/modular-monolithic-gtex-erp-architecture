using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string CardId { get; set; }
        public int ContactId { get; set; }
        public int ReligionId { get; set; }
        public int BloodGroupId { get; set; }
        public string MotherName { get; set; }
        public string MotherNameBangla { get; set; }
        public string FatherName { get; set; }
        public string FatherNameBangla { get; set; }
        public string SpouseName { get; set; }
        public string SpouseNameBangla { get; set; }
        public int NationalityId { get; set; }
        public string BirthRegistrationNo { get; set; }
        public string NationalIdNo { get; set; }
        public string TaxIdentificationNo { get; set; }
        public string DrivingLicenseNo { get; set; }
        public string PassportNo { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? TerminationTypeId { get; set; }
        public int EmpStatusId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public CustomCategory BloodGroup { get; set; }
        public Contact Contact { get; set; }
        public Contact CreatedByContact { get; set; }
        public CustomCategory EmpStatus { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public CustomCategory Nationality { get; set; }
        public CustomCategory Religion { get; set; }
        public CustomCategory TerminationType { get; set; }
    }
}
