//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IAddressRepository
    {
        //this creates an instance of an iqueryable address 
        IQueryable<Address> address { get; }
        /*
        //this creates an instance of a method that allows the address to be saved
        void SaveAddress(int id, Address address);
        */
        
        //this creates an instance that allows address records to be created
        int CreateAddress(Address address);

        //int getID(Address addressModel);
    }
}