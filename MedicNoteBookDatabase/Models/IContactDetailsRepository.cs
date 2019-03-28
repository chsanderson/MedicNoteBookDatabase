using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IContactDetailsRepository
    {
        IQueryable<ContactDetails> ContactDetails { get; }

        void SaveContactDetails(int id, ContactDetails contactDetails);

        /*void*/
        int CreateContactDetails(ContactDetails contactDetails);

        //int getID(ContactDetails contactDetails);
    }
}