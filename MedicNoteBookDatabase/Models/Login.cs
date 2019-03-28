using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MedicNoteBookDatabase.Models
{
    public class Login
    {        
        [Required(ErrorMessage = "There has been an error with your username and/or password")]
        //[EmailAddress]
        public string Username { get; set; }
                
        [Required(ErrorMessage = "There has been an error with your username and/or password")]
        [DataType(DataType.Password)]
        [UIHint("password")]
        public string Password { get; set; }

        //new Added 14/01/2019
        public int[] ids { get; set; }

    }
}
