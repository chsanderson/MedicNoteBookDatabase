using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class ContactDetails
    {
        [BindNever]
        [Key]
        public int ContactDetailsID { get; set; }

        [UIHint("email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string WorkPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        public string NextOfKin { get; set; }

        //[UIHint("email")]
        //public string Email { get; set; }
        //public string HomePhone { get; set; }
        //public string WorkPhone { get; set; }
        //public string MobilePhone { get; set; }
        //public string NextOfKin { get; set; }
    }
}
