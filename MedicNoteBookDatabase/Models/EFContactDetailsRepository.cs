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

        public void SaveContactDetails(int id, ContactDetails contactDetails)
        {
            ContactDetails details = context.ContactDetails.FirstOrDefault(cd => cd.ContactDetailsID == id);
        }

        public /*void*/int  CreateContactDetails(ContactDetails contactDetails)
        {
            //if(ContactDetails.Contains(contactDetails))
            //{
            context.ContactDetails.Add(contactDetails);
            context.SaveChanges();
            return contactDetails.ContactDetailsID;
            //}
        }

        ////this gets the id of the  
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
