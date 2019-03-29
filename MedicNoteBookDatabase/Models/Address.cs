//Christopher Sanderson
//MedicNoteBook
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class Address
    {
        //this defines the string variables that will form the Address Model
        [BindNever]
        [Key]
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Please enter your House Number")]
        public int StreetNumber { get; set; }

        [Required(ErrorMessage = "Please enter your Street Number")]
        public string StreetName { get; set; }

        public string County { get; set; }

        [Required(ErrorMessage = "Please enter your Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Please enter your Postcode")]
        [StringLength(9, MinimumLength = 7 )]
        //[MaxLength = 9 (ErrorMessage = "")]
        public string Postcode { get; set; }
    }
}
