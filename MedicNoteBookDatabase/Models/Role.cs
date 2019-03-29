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
    public class Role
    {
        [BindNever]
        [Key]
        public int ID { get; set; }

        //this allows roles to be entered
        [Required(ErrorMessage = "Role must be entered")]
        [StringLength(10)]
        public string UserRole { get; set; }
    }
}
