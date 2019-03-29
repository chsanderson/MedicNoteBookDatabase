//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFContactDetailsRepository : IContactDetailsRepository
    {
        private ApplicationDBContext context;

        public EFContactDetailsRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public IQueryable<ContactDetails> ContactDetails => context.ContactDetails;

        /*
         * this would be used in future to be able to alter the contact details for a user 
        public void SaveContactDetails(int id, ContactDetails contactDetails)
        {
            ContactDetails details = context.ContactDetails.FirstOrDefault(cd => cd.ContactDetailsID == id);
        }*/

        //this creates a record of the contactDetails model and returns the ID of the record added to the database
        public int  CreateContactDetails(ContactDetails contactDetails)
        {
            context.ContactDetails.Add(contactDetails);
            context.SaveChanges();
            return contactDetails.ContactDetailsID;
        }

        ////this was a previous method that got the id of the record just added
        //public int getID(ContactDetails contactDetails)
        //{
        //    int i = context.ContactDetails.Count();
        //    do
        //    {
        //        if (ContactDetails.ElementAt(i) == contactDetails)
        //        {
        //            i = i;
        //        }
        //        else
        //        {
        //            i--;
        //        }
        //    } while (ContactDetails.ElementAt(i) != contactDetails);
        //    return i;
        //}
    }
}
