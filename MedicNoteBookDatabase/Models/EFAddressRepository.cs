using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFAddressRepository : IAddressRepository
    {
        private ApplicationDBContext context;

        public EFAddressRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public IQueryable<Address> address => context.Address;

        public void SaveAddress(int id, Address address)
        {

        }

        public /*void*/int CreateAddress(Address addressDetails)
        {
            context.Address.Add(addressDetails);
            context.SaveChanges();
            return addressDetails.AddressID;
        }

        //this gets the id of the  
        //public int getID(Address addressModel)
        //{
        //    int i = context.Address.Count();
        //    do
        //    {
        //        if (address.ElementAt(i) == addressModel)
        //        {
        //            i = i;
        //        }
        //        else
        //        {
        //            i--;
        //        }
        //    } while (address.ElementAt(i) != addressModel);
        //    return i;
        //}
    }
}
