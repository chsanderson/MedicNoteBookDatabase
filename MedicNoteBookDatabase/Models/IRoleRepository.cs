//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IRoleRepository
    {
        //this creates an instance of an Iqueryable 
        IQueryable<Role> Role { get; }

        string getRole(int roleID);
    }
}
