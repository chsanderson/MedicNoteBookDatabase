//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicNoteBookDatabase.Models;
using MedicNoteBookDatabase.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace MedicNoteBookDatabase.Controllers
{
    public class AccountController : Controller
    {

        //this creates instances of the IContactDetailsRepository Interface
        private IContactDetailsRepository CDRepository;

        //this creates instances of the IAddressRepository Interface
        private IAddressRepository addressRepository;

        //this creates instances of the IAccountRepository Interface
        private IAccountRepository accountRepository;

        //this creates instances of the IRoleRepository Interface
        private IRoleRepository roleRepository;

        //this creates instances of the IDataProtectionService Interface
        private IDataProtectionService protect; /*IDataProtectionProvider provider*/

        public AccountController(IContactDetailsRepository CDrepo, IAddressRepository repo, IAccountRepository Repository, IRoleRepository roleRepo)
        {
            CDRepository = CDrepo;
            addressRepository = repo;
            accountRepository = Repository;
            roleRepository = roleRepo;
            //protect = protectionService;
            
            //_protector = provider.CreateProtector("MedicNoteBook");//)"c90bab3c-8f97 -461b-af78-16fcfc574edb");//GetType().FullName);
            //_protector = provider.CreateProtector(GetType().FullName);
            //_protector = provider.CreateProtector("key-d3431142-2392-4951-a994-125bf74c8d2b");
        }

        //This will display the createAccount view
        [HttpGet]
        public ViewResult CreateAccount()
        {
            return View();
        }

        //this creates an account and adds it to the database
        [HttpPost]
        public IActionResult CreateAccount(Address address, ContactDetails contactDetails, Account account, string confirmPassword)
        {
            //this checks if the 
            if (account.Password == null || account.Username == null || account.Name == null || account.DOB == null || address.Postcode == null || address.Postcode == null || address.Region == null || address.StreetName == null || address.StreetNumber.ToString() == null || (contactDetails.Email == null && ((contactDetails.HomePhone == null) || (contactDetails.MobilePhone == null) || (contactDetails.WorkPhone == null))))
            {
                if (account.Password == null)
                {
                    TempData["password"] = "Password must be entered for user";
                }

                if (confirmPassword == null)
                {
                    TempData["password"] = "Password must be entered for user";
                }
                if (account.Username == null)
                {
                    TempData["Username"] = "Username must be entered for user";
                }
                if (contactDetails.Email == null && ((contactDetails.HomePhone == null) || (contactDetails.MobilePhone == null) || (contactDetails.WorkPhone == null)))
                {
                    TempData["ContactDetails"] = "Either email or phone number must be entered into the relevant areas";
                }
                string error = "Details must be entered in the relevant sections";
                TempData["Error"] = error;
                return View();
            }
            else
            {
                if (account.Password == confirmPassword && account.Username.Length >= 1 && account.Name.Length >= 4 && account.Password.Length >= 6/* && (account.CHINumber.Length == 10  || account.CHINumber == null || account.CHINumber.Length >= 0 )*/)
                {
                    if (contactDetails.Email.Length >= 6 || (contactDetails.HomePhone.Length >= 11 && contactDetails.HomePhone.Length <= 13) || (contactDetails.MobilePhone.Length >= 11 && contactDetails.MobilePhone.Length <= 13) || (contactDetails.WorkPhone.Length >= 11 && contactDetails.WorkPhone.Length <= 13))
                    {
                        if (address.Postcode.Length >= 6 && address.Postcode.Length <= 9 && address.Region.Length >= 3 && address.StreetName.Length >= 7 && address.StreetNumber > 0)
                        {
                            //IDataProtectionService protect
                            Role role = roleRepository.Role.FirstOrDefault(r => r.UserRole == "Patient");  
                            Account accounts = new Account();
                            //this allows the hashing of password variables
                            using (MD5 hash = MD5.Create())
                            {
                                accounts.Password = GetMd5Hash(hash, account.Password.ToString());
                            }
                            //this attempted to encrypt the password
                            //_protector.Protect(account.Password);
                            //string password = password(account.Password);

                            account.RoleID = role.ID;
                            int[] ids = new int[3];
                            accounts.RoleID = account.RoleID;
                            //this encrypts the string variables if they do not equal null or if they have a value in the accounts model
                            accounts.MedicalPersonnel = Encrypted.encrypt("New Doctor");//protect.Protect("New Doctor");/*Encrypted.encrypt protect.Protect*/ //_protector.Protect("New Doctor");
                            accounts.Name = Encrypted.encrypt(account.Name.ToString());/*Encrypted.encryptprotect.Protect*/ /*protect.Protect*///_protector.Protect(account.Name); //_protector.Protect(account.Name);
                            accounts.Username = Encrypted.encrypt(account.Username.ToString());/*Encrypted.encrypt protect.Protect*/// protect.Protect(account.Username.ToString());//;_protector.Protect(account.Username);
                            if (account.CHINumber != null)
                            {
                                accounts.CHINumber = Encrypted.encrypt(account.CHINumber.ToString());/*Encrypted.encrypt protect.Protect*/// protect.Protect(account.CHINumber.ToString());//_protector.Protect(account.CHINumber);
                            }
                            else
                            {
                                accounts.CHINumber = null;
                            }
                            //this adds the date of birth as it has been 
                            accounts.DOB = account.DOB;// _protector.Protect(account.DOB.ToString());
                            Address addresses = new Address();
                            addresses.StreetNumber = address.StreetNumber;
                            //this encrypts the string variables if they do not equal null or if they have a value in the address model
                            addresses.StreetName = Encrypted.encrypt(address.StreetName.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.StreetName.ToString());// _protector.Protect(address.StreetName);
                            addresses.Region = Encrypted.encrypt(address.Region.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.Region.ToString());//_protector.Protect(address.Region);
                            addresses.Postcode = Encrypted.encrypt(address.Postcode.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.Postcode.ToString());//_protector.Protect(address.Postcode);
                            if (address.County == null)
                            {
                                addresses.County = " ";
                            }
                            else
                            {
                                addresses.County = Encrypted.encrypt(address.County.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.County.ToString());// _protector.Protect(address.County);
                            }

                            ContactDetails cds = new ContactDetails();
                            //this encrypts the string variables if they do not equal null or if they have a value in the contact details model
                            if (contactDetails.Email == null)
                            {
                                cds.Email = contactDetails.Email.ToString();// null;
                            }
                            else
                            {
                                cds.Email = contactDetails.Email.ToString(); //Encrypted.encrypt(contactDetails.Email.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(contactDetails.Email.ToString());// _protector.Protect(contactDetails.Email);
                            }
                            if (contactDetails.HomePhone == null)
                            {
                                cds.HomePhone = null;
                            }
                            else
                            {
                                cds.HomePhone = Encrypted.encrypt(contactDetails.HomePhone.ToString());/*/*Encrypted.encrypt protect.Protect*/// protect.Protect(contactDetails.HomePhone.ToString());// _protector.Protect(contactDetails.HomePhone);
                            }
                            if (contactDetails.WorkPhone != null)
                            {
                                cds.WorkPhone = Encrypted.encrypt(contactDetails.WorkPhone.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(contactDetails.WorkPhone.ToString());// _protector.Protect(contactDetails.WorkPhone);
                            }
                            else
                            {
                                cds.WorkPhone = null;
                            }
                            if (contactDetails.MobilePhone == null)
                            {
                                cds.MobilePhone = null;
                            }
                            else
                            {
                                cds.MobilePhone = Encrypted.encrypt(contactDetails.MobilePhone.ToString());/*/*Encrypted.encrypt protect.Protect*/// protect.Protect(contactDetails.MobilePhone.ToString());// _protector.Protect(contactDetails.MobilePhone);
                            }
                            cds.NextOfKin = Encrypted.encrypt(contactDetails.NextOfKin.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(contactDetails.NextOfKin.ToString());//_protector.Protect(contactDetails.NextOfKin);

                            //this calls the method SaveAccount which creates the account record in the database with the account model supplied
                            accountRepository.SaveAccount(accounts);

                            Account ID = accountRepository.Accounts.FirstOrDefault(a => a.ID == accounts.ID);


                            ids[0] = ID.ID;
                            //this calls the method CreateContactDetails which creates the contact details record in the database with the contact details model supplied and assigns the integer returned to the 2nd entry in the array 
                            ids[1] = CDRepository.CreateContactDetails(cds);
                            //this calls the method CreateAddress which creates the address record in the database with the address model supplied and assigns the integer returned to the 3rd entry in the array 
                            ids[2] = addressRepository.CreateAddress(addresses);


                            string[] stringIDs = new string[3];
                            stringIDs[0] = ids[0].ToString();
                            stringIDs[1] = ids[1].ToString();
                            stringIDs[2] = ids[2].ToString();

                            //this allows the 
                            accountRepository.SetUpAccount(ids[0], accounts, ids[1], ids[2]);
                            
                            //this creates sessions that will be used for the appointments and the medical history
                            HttpContext.Session.setJson("Account", account);
                            HttpContext.Session.setJson("Address", address);
                            HttpContext.Session.setJson("CD", contactDetails);
                            HttpContext.Session.SetString("Name", account.Name);
                            HttpContext.Session.SetString("Type", "Patient");

                            //this redirects the user to the index page of the patient controller
                            return RedirectToAction("Index", "Patient");
                        }
                        else
                        {
                            //this alerts the user to Invalid Address Details
                            TempData["Error"] = "Invalid Address Details";
                            return View();
                        }
                    }
                    else
                    {
                        //this alerts the user that there must be a way to contact the person creating an account
                        TempData["Error"] = "You must enter either Email/HomePhone/MobilePhone/WorkPhone details so you can be contacted";
                        return View();
                    }
                }
                else
                {
                    //this alerts the user to an account has already created
                    TempData["Error"] = "Account Created Already";
                    return View();
                }
            }
        }


    //this returns a string that has been hasshed 
    protected string GetMd5Hash(MD5 md5Hash,string passwords)
    {
        byte[] user = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(passwords));
        StringBuilder stringBuilder = new StringBuilder();
        for(int i = 0; i < user.Length; i++)
        {
            stringBuilder.Append(user[i].ToString("x2"));
        }
        return stringBuilder.ToString();// _protector.Protect(account.Password);// account.Password;
    }

        public ViewResult LogIn()
        {
            return View();
        }

        //public IActionResult AccountView()
        //{
        //    Login login = new Login();
        //    Account account = new Account();
        //    account = HttpContext.Session.getJson<Account>("Account");
        //    if(account != null)
        //    {
        //        login.Username = account.Username;
        //        return View(login);
        //        //return View(login.Username);
        //    }
        //    else
        //    {
        //        login.Username = "";
        //        return View(login);
        //        //login.Username = "";
        //        //return View(login.Username);
        //    }
        //}

        //this checks if the user has entered valid login details
        [HttpPost]
        public IActionResult LogIn(Login login)
        {
            //this checks if username or password has been left empty
            if(login.Password == null || login.Username == null)
            {
                TempData["Error"] = "Require Valid Login Details";
                return View();
            }

            string password;
            //this hashes the password variable temporarily stored in the login model if there is a password
            using (MD5 hash = MD5.Create())
            {
                password = GetMd5Hash(hash, login.Password);
            }
            //string username = /*/*Encrypted.encrypt protect.Protect*/ protect.Protect(login.Username);

            //this assigns the string returned  from validating the user to a new string called username
            string username = accountRepository.validateLogin(login.Username, password, login.Password);
                if (username != " ")// == true) //(login.Username, password) == true)//,login.Password) == true) /*_protector,*///_protector.Protect(login.Username), _protector.Protect(login.Password)) == true)
                {
                    Account account = new Account();
                    Address address = new Address();

                    account = accountRepository.Accounts.FirstOrDefault(u => u.Username == username);//Encrypted.decrypt(u.Username) == login.Username);
                    address = addressRepository.address.FirstOrDefault(a => a.AddressID == account.AddressID);
                    ContactDetails contactDetails = new ContactDetails();
                    contactDetails = CDRepository.ContactDetails.FirstOrDefault(c => c.ContactDetailsID == account.ContactID);
                    try
                    {
                        //this decrypts the string variables that had been encrypted so that the fields can be autofilled when creating an appointment
                        Account accounts = new Account();
                        accounts.RoleID = account.RoleID;
                        accounts.MedicalPersonnel = Encrypted.decrypt(account.MedicalPersonnel);// protect.Protect("New Doctor");/*Encrypted.encrypt protect.Protect*/ //_protector.Protect("New Doctor");
                        accounts.Name = Encrypted.decrypt(account.Name.ToString());/*Encrypted.encryptprotect.Protect*/ /*protect.Protect*///_protector.Protect(account.Name); //_protector.Protect(account.Name);

                        accounts.Username = login.Username;/*Encrypted.encrypt protect.Protect*/ //protect.Protect(account.Username.ToString());//;_protector.Protect(account.Username);
                        if (account.CHINumber != null)
                        {
                            accounts.CHINumber = Encrypted.decrypt(account.CHINumber.ToString());/*Encrypted.encrypt protect.Protect*/ //protect.Protect(account.CHINumber.ToString());//_protector.Protect(account.CHINumber);
                        }
                        else
                        {
                            accounts.CHINumber = null;
                        }
                        accounts.DOB = account.DOB;// _protector.Protect(account.DOB.ToString());
                        Address addresses = new Address();
                        addresses.StreetName = Encrypted.decrypt(address.StreetName.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.StreetName.ToString());// _protector.Protect(address.StreetName);
                        addresses.Region = Encrypted.decrypt(address.Region.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.Region.ToString());//_protector.Protect(address.Region);
                        addresses.Postcode = Encrypted.decrypt(address.Postcode.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.Postcode.ToString());//_protector.Protect(address.Postcode);
                        if (address.County == " ")
                        {
                            addresses.County = " ";
                        }
                        else
                        {
                            addresses.County = Encrypted.decrypt(address.County.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(address.County.ToString());// _protector.Protect(address.County);
                        }
                        ContactDetails cds = new ContactDetails();
                        if (contactDetails.HomePhone == null)
                        {
                            cds.HomePhone = null;
                        }
                        else
                        {
                            cds.HomePhone = Encrypted.decrypt(contactDetails.HomePhone.ToString());/*/*Encrypted.encrypt protect.Protect*/// protect.Protect(contactDetails.HomePhone.ToString());// _protector.Protect(contactDetails.HomePhone);
                        }
                        if (contactDetails.WorkPhone != null)
                        {
                            cds.WorkPhone = Encrypted.decrypt(contactDetails.WorkPhone.ToString());/*/*Encrypted.encrypt protect.Protect*/ //protect.Protect(contactDetails.WorkPhone.ToString());// _protector.Protect(contactDetails.WorkPhone);
                        }
                        else
                        {
                            cds.WorkPhone = null;
                        }
                        if (contactDetails.MobilePhone == null)
                        {
                            cds.MobilePhone = null;
                        }
                        else
                        {
                            cds.MobilePhone = Encrypted.decrypt(contactDetails.MobilePhone.ToString());/*/*Encrypted.encrypt protect.Protect*/// protect.Protect(contactDetails.MobilePhone.ToString());// _protector.Protect(contactDetails.MobilePhone);
                        }
                        cds.NextOfKin = Encrypted.decrypt(contactDetails.NextOfKin.ToString());
                        /*if (contactDetails.Email == null)
                        {
                            cds.Email = null;// contactDetails.Email.ToString();// null;
                        }
                        else
                        {
                        //this is an example of trying to decrypt an email string but was unsuccessful as it caused an error
                            string email = Encrypted.decrypt(contactDetails.Email);
                            cds.Email = email;//Encrypted.decrypt(contactDetails.Email.ToString());/*/
                        //Encrypted.encrypt protect.Protect*/ //protect.Protect(contactDetails.Email.ToString());// _protector.Protect(contactDetails.Email);
                        //}

                        //if the string decryption could not be completed then the models would be passed to the session
                        cds.Email = contactDetails.Email.ToString();
                        HttpContext.Session.setJson("Account", accounts);
                        HttpContext.Session.setJson("Address", addresses);
                        HttpContext.Session.setJson("CD", cds);
                        HttpContext.Session.SetString("Name", accounts.Name);
                        string role = roleRepository.getRole(accounts.RoleID);
                        HttpContext.Session.SetString("Type", role);
                    }
                    catch
                    {
                        //if the string decryption could not be completed then the models would be passed to the session
                        HttpContext.Session.setJson("Account", account);
                        HttpContext.Session.setJson("Address", address);
                        HttpContext.Session.setJson("CD", contactDetails);
                        HttpContext.Session.SetString("Name", account.Name);
                        string role = roleRepository.getRole(account.RoleID);
                        HttpContext.Session.SetString("Type", role);
                    }
                //this checks if the user is a patient or not and redirects them to the appropriate home page
                    if (account.RoleID == 3 || account.RoleID == 4 || account.RoleID == 6)
                    {
                        return RedirectToAction("Index", "Practitioners");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Patient");
                    }
                    //return RedirectToPage();
                    ////address = addressRepository.address.Where(a => a.AddressID == id[2]);
                    //return RedirectToAction("~/Views/Patient/Index?ids=" + id);
                    /*return RedirectToAction("../Patient/Index");*//*, /*accountRepository.Accounts.Where(l => l.ID == id[0]), /*); 23:11 13/01/2019, addressRepository.address.Where(l => l.AddressID == id[1]), CDRepository.ContactDetails.Where(cd => cd.ContactDetailsID == id[2])));/*account.ContactID
                    /*,accountRepository.Accounts.Where(l => l.ID == id[0])*///);// 23:11 13/01/2019, addressRepository.address.Where(l => l.AddressID == id[1]), CDRepository.ContactDetails.Where(cd => cd.ContactDetailsID == id[2])));/*account.ContactID*/
                }
                else
                {
                    //this creates a temporary message that tells the user that their login details are incorrect and returns the view to the user
                    TempData["Error"] = "Login Details Incorrect";
                    return View();
                }
        }

        public IActionResult LogOut()
        {
            //this clears all of the sessions that were created when the user logged in and redirects the user to the index view in the home controller
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}