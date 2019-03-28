using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IRoleRepository
    {
        IQueryable<Role> Role { get; }

        string getRole(int roleID);
    }
}
