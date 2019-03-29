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
    public class PatientAppointmentReferral
    {
        [BindNever]
        [Key]
        public int PatientApplicationReferralID { get; set; }

        //[Required(ErrorMessage = "Please enter your symptoms")]
        public string Symptoms { get; set; }

        public string Decision { get; set; }
        
        //[Required(ErrorMessage = "Please enter a time between 9am and 16:00")]
        //[Display(Name = "Requested Time")]
        [DataType(DataType.Time)]
        public string RequestedTime { get; set; }

        //[Required(ErrorMessage = "Please select this date or onwards")]
        [Display(Name = "Requested Date")]
        [DataType(DataType.Date)]
        //[Display( = DateTime.Now)]
        public DateTime RequestedDate { get; set; }

        //[Required(ErrorMessage = "Please enter your symptoms")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        //[Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter your Name")]
        public string MedicalPersonnel { get; set; }

        [Display(Name = "Current Date")]
        [DataType(DataType.Date)]
        public DateTime CurrentDate { get; set; }

        [Display(Name = "Street Number")]
        //[Required(ErrorMessage = "Please enter your House Number")]
        public int StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        //[Required(ErrorMessage = "Please enter your Street Number")]
        public string StreetName { get; set; }

        public string County { get; set; }

        //[Required(ErrorMessage = "Please enter your Region")]
        public string Region { get; set; }

        //[Required(ErrorMessage = "Please enter your Postcode")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }

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
    }
}
