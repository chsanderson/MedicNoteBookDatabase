using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicNoteBookDatabase.Controllers;
using MedicNoteBookDatabase.Infrastructure;
using Microsoft.AspNetCore.DataProtection;

namespace MedicNoteBookDatabase.Models
{
    public class EFAccountRepository: IAccountRepository
    {
        private ApplicationDBContext DBcontext;
        private IRoleRepository roleRepository;
        //private IDataProtector _protector;
        private IDataProtectionService protectionService;

        public EFAccountRepository(ApplicationDBContext context, IRoleRepository roleRepo, IDataProtectionProvider provider)
        {//, IDataProtectionProvider provider)
            DBcontext = context;
            roleRepository = roleRepo;
           // _protector = provider.CreateProtector("c90bab3c-8f97 -461b-af78-16fcfc574edb");//GetType().FullName);
            //_protector = provider.CreateProtector("key-d3431142-2392-4951-a994-125bf74c8d2b");
        }

        public IQueryable<Account> Accounts => DBcontext.Account;

        public void /*int*/ SaveAccount(Account account)
        {
            if (Accounts.Contains(account) == false)
            {
                DBcontext.Account.Add(account);
                DBcontext.SaveChanges();
            }
            //return account.ID;
        }

        public void SetUpAccount(int id, Account account, int ContactDetailsID, int AddressID)
        {
            //if(Accounts.Where(a => a.ID == account.ID))
            //{
            Role role = roleRepository.Role.FirstOrDefault(r => r.UserRole == "Patient"); 
            Account details = DBcontext.Account.FirstOrDefault(a => a.ID == id);
            details.Name = account.Name;
            details.Password = account.Password;
            details.Username = account.Username;
            details.DOB = account.DOB;
            details.AddressID = AddressID;
            details.ContactID = ContactDetailsID;
            details.CHINumber = account.CHINumber;
            details.RoleID = role.ID;
            //}
            DBcontext.SaveChanges();
        }
/*IDataProtector _protector,*/
        public string validateLogin(string username, string password, string passwordAlt)//(Login login)
        {/*bool
            bool*/
            string validateUser = " ";
            //Account validate = DBcontext.Account.FirstOrDefault(a => a.Username == username);
            string[] passwords = new string[Accounts.Count()];
            string[] usernames = new string[Accounts.Count()];
            List<Account> accountDetails = new List<Account>();
            accountDetails = Accounts.ToList();
            int count = Accounts.Count();
            for (int j = 0; j < count;j++)
            {
                usernames[j] = accountDetails[j].Username.ToString();
                passwords[j] = accountDetails[j].Password.ToString();
            }
            int i = 0;
            do
            {
                try
                {
                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                    var user = Encrypted.decrypt(usernames[i].ToString()).ToString();//protectionService.UnProtect(usernames[i]);/*protectionService.UnProtect(usernames[i])*/ 
                    if (user == username)
                    {
                        if (0 == comparer.Compare(password, passwords[i]))
                        {
                            validateUser = usernames[i];//true;   
                            return validateUser;
                        }
                        else
                        {
                            validateUser = " ";//validateUser = false;
                            //i++;
                        }
                    }
                }
                catch
                {
                    if (usernames[i] == username && (passwords[i] == password || passwords[i] == passwordAlt))
                    {
                        validateUser = usernames[i];//true;
                        return validateUser;
                    }
                    else
                    {
                        validateUser = " ";// false;
                        //i++;
                    }
                }
                i++;
            } while (/*validateUser == " " ||*/ i < count);/*false*/
            validateUser = " ";
            Account validate = DBcontext.Account.FirstOrDefault(a => a.Username == username);
            //Account validate = DBcontext.Account.FirstOrDefault(a => Encrypted.decrypt(a.Username) == username);//_protector.Unprotect()
            //Account validate = DBcontext.Account.FirstOrDefault(a => a.Username == _protector.Protect(username));
            /*if (validate.Username == username)//&& validate.Password == password)/*_protector.Unprotect()*//*_protector.Unprotect(*//*)*///(validate.Username == _protector.Protect(username) && validate.Password == _protector.Protect(password)) //if(validate.Password.Equals(login.Password))
            /*{
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                if(0 == comparer.Compare(password, validate.Password))
                {
                    validateUser = true;
                }
                else
                {
                    validateUser = false;
                }
            }
            else
            {
                validateUser = false;
            }*/
            return validateUser;
        }

        public /*int[]*/ /*void*/ int[] getID(string username, string password)//(Login login)
        {
            int[] validateUser = new int[3];
            Account validate = DBcontext.Account.FirstOrDefault(a => a.Username == username);
            //if (validate.Password == password)//if(validate.Password.Equals(login.Password))
            //{
                validateUser[0] = validate.ID;
                validateUser[1] = validate.AddressID;
                validateUser[2] = validate.ContactID;
                //Account accounts = new Account();
                //accounts.ID = validate.ID;
                //accounts.Name = validate.Name;
                //accounts.Username = username;
                //accounts.AddressID = validate.AddressID;
                //accounts.ContactID = validate.ContactID;
                //accounts.DOB = validate.DOB;
                //accounts.CHINumber = validate.CHINumber;

                //ContactDetails CD = DBcontext.ContactDetails.FirstOrDefault(c => c.ContactDetailsID == accounts.ContactID);
                //ContactDetails contactDetails = new ContactDetails();
                //contactDetails.ContactDetailsID = CD.ContactDetailsID;
                //contactDetails.Email = CD.Email;
                //contactDetails.HomePhone = CD.HomePhone;
                //contactDetails.MobilePhone = CD.MobilePhone;
                //contactDetails.WorkPhone = CD.WorkPhone;
                //contactDetails.NextOfKin = CD.NextOfKin;

                //Address AddressDetails = DBcontext.Address.FirstOrDefault(a => a.AddressID == accounts.AddressID);
                //Address address = new Address();
                //address.AddressID = AddressDetails.AddressID;
                //address.County = AddressDetails.County;
                //address.StreetNumber = AddressDetails.StreetNumber;
                //address.StreetName = AddressDetails.StreetName;
                //address.Region = AddressDetails.Region;
                //address.Postcode = AddressDetails.Postcode;
                //return validateUser;
            //}
            //else
            /*{
                validateUser[0] = 0;
                validateUser[1] = 0;
                validateUser[2] = 0;
            }*/
            return validateUser;
        }

        public void UpdateAccount(int id, Account account)
        {
            //if(Accounts.Where(a => a.ID == account.ID))
            //{

            Account details = DBcontext.Account.FirstOrDefault(a => a.ID == id);
            details.Name = account.Name;
            details.Password = account.Password;
            details.Username = account.Username;
            details.DOB = account.DOB;
            details.AddressID = account.AddressID;
            details.ContactID = account.ContactID;
            details.CHINumber = account.CHINumber;
            //}
            DBcontext.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            if(Accounts.Contains(account) == true)
            {
                DBcontext.Account.Remove(account);
            }
            DBcontext.SaveChanges();
        }

        //public int getID(Account account)
        //{
        //    int i = DBcontext.Account.Count();
        //    //do
        //    //{
        //    //    if (Accounts. == account)
        //    //    {
        //    //        i = i;
        //    //    }
        //    //    else
        //    //    {
        //    //        i--;
        //    //    }
        //    //} while (Accounts.ElementAt(i) != account);
        //    return i;
        //}
    }
}
