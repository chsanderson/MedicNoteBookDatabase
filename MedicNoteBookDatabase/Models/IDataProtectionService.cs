using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    interface IDataProtectionService
    {
        string Protect(string value);

        string UnProtect(string value);
    }
}
