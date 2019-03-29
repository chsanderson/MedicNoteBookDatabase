//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    //this attempts to encrypt and decrypt the 
    interface IDataProtectionService
    {
        string Protect(string value);

        string UnProtect(string value);
    }
}
