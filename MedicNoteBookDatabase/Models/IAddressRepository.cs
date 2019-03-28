using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IAddressRepository
    {
        IQueryable<Address> address { get; }

        void SaveAddress(int id, Address address);

        /*void*/
        int CreateAddress(Address address);

        //int getID(Address addressModel);
    }
}