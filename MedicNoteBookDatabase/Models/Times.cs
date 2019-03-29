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
    public class Times
    {
        [Key]
        [BindNever]
        public int ID { get; set; }

        //This sets a string variable called time to store time variables 
        [DataType(DataType.Time)]
        public string Time { get; set; }

        //[DataType(DataType.Time)]
        //public string DateTime { get; set; }
    }
}
