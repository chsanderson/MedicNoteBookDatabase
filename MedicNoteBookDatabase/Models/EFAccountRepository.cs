//Christopher Sanderson
//MedicNoteBook
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
        //private IDataProtectionService protectionService;

        public EFAccountRepository(ApplicationDBContext context, IRoleRepository roleRepo, IDataProtectionProvider provider)
        {//, IDataProtectionProvider provider)
            DBcontext = context;
            roleRepository = roleRepo;
           // _protector = provider.CreateProtector("c90bab3c-8f97 -461b-af78-16fcfc574edb");//GetType().FullName);
            //_protector = provider.CreateProtector("key-d3431142-2392-4951-a994-125bf74c8d2b");
        }

        //
        public IQueryable<Account> Accounts => DBcontext.Account;

        //this saves the account to the database
        public void SaveAccount(Account account)
        {
            if (Accounts.Contains(account) == false)
            {
                DBcontext.Account.Add(account);
                DBcontext.SaveChanges();
            }
            //return account.ID;
        }

        //this is a method that updates the account when created to add the address and ContactDetails' ids
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

        //this vlaidates the user whether they have encrypted, hashed or normal records displayed in the database and returns the username as a string variable
        public string validateLogin(string username, string password, string passwordAlt)
        {
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
                    //this creates the string variable comparer
                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                    //this checks
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
            } while (i < count);
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

        //this returns an int array that contains the ID for the Account, Address and ContactDetails record
        public int[] getID(string username, string password)//(Login login)
        {
            int[] validateUser = new int[3];
            Account validate = DBcontext.Account.FirstOrDefault(a => a.Username == username);
                validateUser[0] = validate.ID;
                validateUser[1] = validate.AddressID;
                validateUser[2] = validate.ContactID;
            return validateUser;
        }

        //this will be used at a later date to update the account information
        /*public void UpdateAccount(int id, Account account)
        {
            Account details = DBcontext.Account.FirstOrDefault(a => a.ID == id);
            details.Name = account.Name;
            details.Password = account.Password;
            details.Username = account.Username;
            details.DOB = account.DOB;
            details.AddressID = account.AddressID;
            details.ContactID = account.ContactID;
            details.CHINumber = account.CHINumber;
            DBcontext.SaveChanges();
        }
        */
        //this deletes the account although this will be used at a later date
        /*public void DeleteAccount(Account account)
        {
            if(Accounts.Contains(account) == true)
            {
                DBcontext.Account.Remove(account);
            }
            DBcontext.SaveChanges();
        }*/

        //this is an older instance of returning the account ID
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
