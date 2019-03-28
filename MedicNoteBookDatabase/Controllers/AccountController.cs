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
        private IContactDetailsRepository CDRepository;
        private IAddressRepository addressRepository;
        private IAccountRepository accountRepository;
        private IRoleRepository roleRepository;
        private IDataProtectionService protect;
        /*private*/
        //IDataProtector _protector;



/*IDataProtectionProvider provider*/
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

        //This will be able to 
        [HttpGet]
        public ViewResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Address address, ContactDetails contactDetails, Account account, string confirmPassword)
        {
            //Account accountCreate = accountRepository.Accounts.Where(a => a.Name == account.Name).Where(a => a.DOB == account.DOB);
            //if(accountRepository.Accounts.Where(a => a.Name == account.Name).Where(a => a.DOB == account.DOB) == null)
            //Account accountCheck = new Account();
            //accountCheck = accountRepository.Accounts.FirstOrDefault(a => a.Name == account.Name);//accountRepository.Accounts.Where(a => a.Name == account.Name);//.Where(a => a.DOB == account.DOB);
            //if (accountCheck == null || accountCheck.DOB != account.DOB || accountCheck.DOB == null) 
            //{
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
                    //temp = error + "and the contact details section";
                    TempData["ContactDetails"] = "Either email or phone number must be entered into the relevant areas";
                    //temp = "";
                }
                // if (address.Postcode == null || address.Postcode == null || address.Region == null || address.StreetName == null || address.StreetNumber.ToString() == null)
                //{
                //    temp = error + " and the address section";
                //    TempData["Error"] = error;
                //    temp = "";
                //}
                string error = "Details must be entered in the relevant sections";
                TempData["Error"] = error;
                return View();
            }
            //}
            //if (contactDetails.Email == null || (contactDetails.HomePhone == null) || (contactDetails.MobilePhone == null) || (contactDetails.WorkPhone == null))
            //{
            //    string temp;
            //    string error = "Details must be entered in the contact details section";
            //    if (account.Password == null || account.Username == null || account.Name == null || account.DOB == null)
            //    {
            //        temp = error + "and the account section";
            //        TempData["Error"] = error;
            //        temp = "";
            //    }
            //    if (address.Postcode == null || address.Postcode == null || address.Region == null || address.StreetName == null || address.StreetNumber.ToString() == null)
            //    {
            //        temp = error + " and the address section";
            //        TempData["Error"] = error;
            //        temp = "";
            //    }               
            //    TempData["Error"] = error;
            //    return View();
            //}
            //if (address.Postcode == null || address.Postcode == null || address.Region == null || address.StreetName == null || address.StreetNumber.ToString() == null)
            //{
            //    string temp;
            //    string error = "Details must be entered in the address section";
            //    if (account.Password == null || account.Username == null || account.Name == null || account.DOB == null)
            //    {
            //        temp = error + "and the account section";
            //        TempData["Error"] = error;
            //        temp = "";
            //    }
            //    if (contactDetails.Email == null || (contactDetails.HomePhone == null) || (contactDetails.MobilePhone == null) || (contactDetails.WorkPhone == null))
            //    {
            //        temp = error + " and the contact details section";
            //        TempData["Error"] = error;
            //        temp = "";
            //    }
            //    TempData["Error"] = error;
            //    return View();
            //}
            else
            {
                if (account.Password == confirmPassword && account.Username.Length >= 1 && account.Name.Length >= 4 && account.Password.Length >= 6/* && (account.CHINumber.Length == 10  || account.CHINumber == null || account.CHINumber.Length >= 0 )*/)
                {
                    //if(ModelState.IsValid)
                    //{
                    //account.Password = account.Password.
                    if (contactDetails.Email.Length >= 6 || (contactDetails.HomePhone.Length >= 11 && contactDetails.HomePhone.Length <= 13) || (contactDetails.MobilePhone.Length >= 11 && contactDetails.MobilePhone.Length <= 13) || (contactDetails.WorkPhone.Length >= 11 && contactDetails.WorkPhone.Length <= 13))
                    {
                        if (address.Postcode.Length >= 6 && address.Postcode.Length <= 9 && address.Region.Length >= 3 && address.StreetName.Length >= 7 && address.StreetNumber > 0)
                        {
                            //IDataProtectionService protect
                            Role role = roleRepository.Role.FirstOrDefault(r => r.UserRole == "Patient");
                            account.RoleID = role.ID;
                            int[] ids = new int[3];
                            Account accounts = new Account();
                            accounts.RoleID = account.RoleID;
                            accounts.MedicalPersonnel = Encrypted.encrypt("New Doctor");// protect.Protect("New Doctor");/*Encrypted.encrypt protect.Protect*/ //_protector.Protect("New Doctor");
                             accounts.Name = Encrypted.encrypt(account.Name.ToString());/*Encrypted.encryptprotect.Protect*/ /*protect.Protect*///_protector.Protect(account.Name); //_protector.Protect(account.Name);
                            //string password = password(account.Password);

                            using (MD5 hash = MD5.Create())
                            {
                                accounts.Password = GetMd5Hash(hash, account.Password.ToString());
                            }//_protector.Protect(account.Password);
                            accounts.Username = Encrypted.encrypt(account.Username.ToString());/*Encrypted.encrypt protect.Protect*/// protect.Protect(account.Username.ToString());//;_protector.Protect(account.Username);
                            if (account.CHINumber != null)
                            {
                                accounts.CHINumber = Encrypted.encrypt(account.CHINumber.ToString());/*Encrypted.encrypt protect.Protect*/// protect.Protect(account.CHINumber.ToString());//_protector.Protect(account.CHINumber);
                            }
                            else
                            {
                                accounts.CHINumber = null;
                            }
                            accounts.DOB = account.DOB;// _protector.Protect(account.DOB.ToString());
                            Address addresses = new Address();
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
                            //string temp = GetMd5Hash(account.Password);
                            //account.Password = temp;
                            accountRepository.SaveAccount(accounts);
                            //ids[0] = 
                            //accountRepository.SaveAccount(account);
                            //accountRepository.SaveAccount(account);
                            Account ID = accountRepository.Accounts.FirstOrDefault(a => a.ID == accounts.ID);
                            //Account ID = accountRepository.Accounts.FirstOrDefault(a => a.ID == account.ID);
                            ids[0] = ID.ID;
                            ids[1] = CDRepository.CreateContactDetails(cds);
                            ids[2] = addressRepository.CreateAddress(addresses);
                            //ids[1] = CDRepository.CreateContactDetails(contactDetails);
                            //ids[2] = addressRepository.CreateAddress(address);

                            string[] stringIDs = new string[3];
                            stringIDs[0] = ids[0].ToString();
                            stringIDs[1] = ids[1].ToString();
                            stringIDs[2] = ids[2].ToString();

                            //int AccountID = accountRepository.SaveAccount(account);
                            //int ContactDetailsID = CDRepository.CreateContactDetails(contactDetails);
                            //int AddressID = addressRepository.CreateAddress(address);
                            /*int AccountID = accountRepository.getID(account);
                            int ContactDetailsID = CDRepository.getID(contactDetails);
                            int AddressID = addressRepository.getID(address);*/
                            //accountRepository.SetUpAccount(AccountID, account, ContactDetailsID, AddressID);
                            accountRepository.SetUpAccount(ids[0], accounts, ids[1], ids[2]);

                            ////ISession session;
                            //ISession sessions = new ISession();
                            //Sessions.setJson(session, "ids", stringIDs);

                            HttpContext.Session.setJson("Account", account);
                            HttpContext.Session.setJson("Address", address);
                            HttpContext.Session.setJson("CD", contactDetails);
                            HttpContext.Session.SetString("Name", account.Name);
                            HttpContext.Session.SetString("Type", "Patient");


                            //HttpContext.Session.SetString("Account", stringIDs[0]);
                            //HttpContext.Session.SetString("Address", stringIDs[1]);
                            //HttpContext.Session.SetString("CD", stringIDs[2]);
                            //HttpContext.Session.setJson("ids", stringIDs);
                            //Sessions.setJson(, "login", stringIDs);
                            //Sessions.setJson("ids",1,ids.ToString);
                            //return RedirectToAction("~/Views/Patient/Index");
                            return RedirectToAction("Index", "Patient");
                        }
                        else
                        {
                            TempData["Error"] = "Invalid Address Details";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Error"] = "You must enter either Email/HomePhone/MobilePhone/WorkPhone details so you can be contacted";
                        return View();
                    }
                }
                else
                {
                    TempData["Error"] = "Account Created Already";
                    return View();
                }
            }
            //}
            //else
            //{
            //    TempData["Error"] = "Account Already Exists";
            //    return RedirectToAction("Index","Home");
            //}
        }

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

        [HttpPost]
        public IActionResult LogIn(Login login)
        {
            if(login.Password == null || login.Username == null)
            {
                TempData["Error"] = "Require Valid Login Details";
                return View();
            }
            string password;
            using (MD5 hash = MD5.Create())
            {
                password = GetMd5Hash(hash, login.Password);
            }
            //string username = /*/*Encrypted.encrypt protect.Protect*/ protect.Protect(login.Username);
            string username = accountRepository.validateLogin(login.Username, password, login.Password);
                if (username != " ")// == true) //(login.Username, password) == true)//,login.Password) == true) /*_protector,*///_protector.Protect(login.Username), _protector.Protect(login.Password)) == true)
                {/*/*protect, */
                 //1/301/2019
                 //14/01/2019
                 //int[] id = new int[3];
                 /*id = */
                 //Added 13012019 updated 14012019
                 //id = accountRepository.getID(login.Username, login.Password);
                 //13012019 + 14012019
                 //int id = accountRepository.getID(login);
                 //id = accountRepository.getID(login);
                 // = account.AddressID;
                 //addressRepository.address.Where(l => l.AddressID == addressRepository.address.FirstOrDefault().AddressID);/*login.Username*//*account.AddressID*/

                Account account = new Account();
                    Address address = new Address();

                account = accountRepository.Accounts.FirstOrDefault(u => u.Username == username);//Encrypted.decrypt(u.Username) == login.Username);
                    address = addressRepository.address.FirstOrDefault(a => a.AddressID == account.AddressID);
                    ContactDetails contactDetails = new ContactDetails();
                contactDetails = CDRepository.ContactDetails.FirstOrDefault(c => c.ContactDetailsID == account.ContactID);
                try
                {
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
                        string email = Encrypted.decrypt(contactDetails.Email);
                        cds.Email = email;//Encrypted.decrypt(contactDetails.Email.ToString());/*/
                    //Encrypted.encrypt protect.Protect*/ //protect.Protect(contactDetails.Email.ToString());// _protector.Protect(contactDetails.Email);
                    //}
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
                    //account = accountRepository.Accounts.FirstOrDefault(u => Encrypted.decrypt(u.Username) == login.Username);// u.ID == id[0]);


                    HttpContext.Session.setJson("Account", account);
                    HttpContext.Session.setJson("Address", address);
                    HttpContext.Session.setJson("CD", contactDetails);
                    HttpContext.Session.SetString("Name", account.Name);
                    string role = roleRepository.getRole(account.RoleID);
                    HttpContext.Session.SetString("Type", role);
                }
                    //account = accountRepository.Accounts.Where(u => u.ID == id[0]);
                    //ContactDetails contactDetails = new ContactDetails();
                    //contactDetails = CDRepository.ContactDetails.FirstOrDefault(c => c.ContactDetailsID == id[2]);
                    //contactDetails = CDRepository.ContactDetails.Where(c => c.ContactDetailsID == id[1]);
                    //Address address = new Address();
                    //address = addressRepository.address.FirstOrDefault(a => a.AddressID == id[1]);

                    //HttpContext.Session.setJson("Account", account);
                    //HttpContext.Session.setJson("Address", address);
                    //HttpContext.Session.setJson("CD", contactDetails);
                    //HttpContext.Session.SetString("Name", account.Name);
                    //string role = roleRepository.getRole(account.RoleID);
                    //HttpContext.Session.SetString("Type", role);

                    //HttpContext.Session.SetString("Account", id[0].ToString());
                    //HttpContext.Session.SetString("Address", id[1].ToString());
                    //HttpContext.Session.SetString("CD", id[2].ToString());
                    //AccountView();
                    if (account.RoleID == 3 || account.RoleID == 4 || account.RoleID == 6)//role != null || role != "Patient" || role != "3")
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
                    //AccountView();
                    TempData["Error"] = "Login Details Incorrect";
                    return View();
                }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
        //    Account account = new Account();
        //    ContactDetails Contact = new ContactDetails();
        //    Address address = new Address();
        //    Login login = new Login();
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("../../Home/Index");
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}