//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFRoleRepository : IRoleRepository
    {
        private ApplicationDBContext context;
        
        public EFRoleRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public IQueryable<Role> Role => context.Roles;

        public string getRole(int roleID)
        {
            Role role = Role.FirstOrDefault(r => r.ID == roleID);
            //int roleID = 0;
            if(role != null)
            {
                return role.UserRole;
            }
            else
            {
                return "An error has occurred";
            }
        }
    }
}
