using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class PracticeInfo
    {
        [BindNever]
        [Key]
        public int PracticeInfoID { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Region { get; set; }
    }
}
