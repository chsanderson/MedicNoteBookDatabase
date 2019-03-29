//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IContactDetailsRepository
    {
        IQueryable<ContactDetails> ContactDetails { get; }
        /*
         * this would be used in future to be able to alter the contact details for a user 
        //this creates an instance of a method which allows the editing of contact details
        void SaveContactDetails(int id, ContactDetails contactDetails);
        */

        /*void*/
        //this creates an instance of a method that allows contact details to be created 
        int CreateContactDetails(ContactDetails contactDetails);

        //this is a previous instance of a method that returned the id of the last record
        //int getID(ContactDetails contactDetails);
    }
}