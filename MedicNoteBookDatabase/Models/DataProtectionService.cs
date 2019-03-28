using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class DataProtectionService: IDataProtectionService
    {
        private readonly IDataProtector protector;

        public DataProtectionService(IDataProtectionProvider provider)
        {
            protector = provider.CreateProtector("c90bab3c-8f97-461b-af78-16fcfc574edb");
        }

        public string Protect(string value)
        {
            return protector.Protect(value);
        }

        public string UnProtect(string value)
        {
            return protector.Unprotect(value);
        }
    }
}
