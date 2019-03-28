using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class Appointment
    {
        [BindNever]
        [Key]
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Please enter Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Please enter Appointment Time")]
        public string AppointmentTime { get; set; }

        [Required(ErrorMessage = "Please enter the symptoms")]
        public string Symptoms { get; set; }
        
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Please enter the Patient's Full Name")]
        public string PatientFullName { get; set; }

        [Required(ErrorMessage = "Please enter a Medical Professional")]
        public string AppointmentMedicalProfessional { get; set; }
        
        public int UserReferralID { get; set; }

        [Required(ErrorMessage = "Please enter the Appointment Type")]
        public string AppointmentType { get; set; }
               
        [Required(ErrorMessage = "Please enter the Patient's Date of Birth")]
        public DateTime DOB { get; set; }

        public DateTime CurrentDate { get; set; }

        [Required(ErrorMessage = "Please enter the Patient's House Number")]
        public int StreetNumber { get; set; }

        [Required(ErrorMessage = "Please enter the Patient's Street Number")]
        public string StreetName { get; set; }

        public string County { get; set; }

        [Required(ErrorMessage = "Please enter the Patient's Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Please enter the Patient's Postcode")]
        public string Postcode { get; set; }
    }
}
