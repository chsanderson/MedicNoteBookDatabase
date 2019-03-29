//Christopher Sanderson
//MedicNoteBook
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MedicNoteBookDatabase.Infrastructure;

namespace MedicNoteBookDatabase.Models
{
    public class Account
    {
        //this defines the string variables that will form the Account Model
        [BindNever]
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter your Date Of Birth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
       
        //[Required]
        [StringLength(10)]
        public string CHINumber { get; set; }

        //[Required]
        public string Username { get; set; }

        //[Required]
        public string MedicalPersonnel { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [UIHint("password")]
        public string Password { get; set; }
        
        //[Required]
        public int AddressID { get; set; }
        
        //[Required]
        public int ContactID { get; set; }

        public int RoleID { get; set; }
    }
}
