//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicNoteBookDatabase.Models;
using Microsoft.AspNetCore.Http;
using MedicNoteBookDatabase.Infrastructure;
using Microsoft.AspNetCore.DataProtection;

namespace MedicNoteBook.Controllers
{
    public class AppointmentController : Controller
    {
        private IPatientAppointmentReferralRepository PARrepository;
        private IAccountRepository AccountRepository;
        private IAddressRepository AddressRepository;
        private IContactDetailsRepository CDRepository;
        private IAppointmentRepository AppointmentRepository;
        private ITimesRepository timesRepository;
        //private IDataProtector _protector;IDataProtectionProvider provider ,

        public AppointmentController(IPatientAppointmentReferralRepository PARrepo, IAccountRepository accountRepo, IAddressRepository AddressRepo, IContactDetailsRepository CDRepo, IAppointmentRepository appointmentRepo, ITimesRepository timesRepo)
        {
            PARrepository = PARrepo;
            AccountRepository = accountRepo;
            AddressRepository = AddressRepo;
            CDRepository = CDRepo;
            AppointmentRepository = appointmentRepo;
            timesRepository = timesRepo;
        }

        //this displays the 
        public ViewResult Index()
        {
            if(HttpContext.Session.GetString("Type") == "Patient")
            {
                return View("Index", "Patient");
            }
            else if (HttpContext.Session.GetString("Type") == "Doctor" || HttpContext.Session.GetString("Type") == "Nurse")
            {
                return View("Index", "Practitioners");
            }
            else
            {
                if (HttpContext.Session.GetString("Name") == null)
                {
                List<string> accounts = new List<string>();
                    List<Account> doctors = new List<Account>();
                    doctors = AccountRepository.Accounts.Where(a => a.RoleID == 3).ToList();
                    //accounts.Add("None");
                    foreach (var item in doctors)
                    {
                        //accounts.Add(item.MedicalPersonnel.ToString());
                        accounts.Add(item.Name.ToString());
                    }
                    ViewBag.doctorNo = accounts.Count();
                    ViewBag.doctors = accounts;
                } 
                return View();
            }
        }

        //[HttpGet]
        //this loads the appointment view to create an appointment
        [HttpPost]
        public ViewResult CreateAppointment(string MedicalPersonnel)
        {
            
                PatientAppointmentReferral patientAppointmentReferral = new PatientAppointmentReferral();
            patientAppointmentReferral.MedicalPersonnel = MedicalPersonnel;
            try
            {
                if(HttpContext.Session.GetString("Type") == "Receptionist" || HttpContext.Session.GetString("Type") == "Nurse" || HttpContext.Session.GetString("Type") == "Doctor" || HttpContext.Session.GetString("Type") == null)
                {
                    patientAppointmentReferral.Name = "Julie Doe";
                    patientAppointmentReferral.DOB = new DateTime(1980, 6, 25, 0, 0, 0);
                    patientAppointmentReferral.County = "Lanarkshire";
                    patientAppointmentReferral.StreetNumber = 0;
                    patientAppointmentReferral.StreetName = "Main Road";
                    patientAppointmentReferral.Region = "Strathclyde";
                    patientAppointmentReferral.Symptoms = "Sore Head, Pains in Stomach";
                    patientAppointmentReferral.CurrentDate = DateTime.Now;
                    patientAppointmentReferral.Postcode = "G01 145";
                    patientAppointmentReferral.Email = "Ju1ieDoe@example.com";
                    patientAppointmentReferral.HomePhone = "0141 123 4567";
                    patientAppointmentReferral.WorkPhone = "0123 567 8910";
                    patientAppointmentReferral.MobilePhone = "01234 567890";
                    patientAppointmentReferral.NextOfKin = "John Doe";
                    return View("CreateAppointment", patientAppointmentReferral);
                }
                else
                {

                    Account account = new Account();
                    account = HttpContext.Session.getJson<Account>("Account");
                    Address address = new Address();
                    address = HttpContext.Session.getJson<Address>("Address");
                    ContactDetails contactDetails = new ContactDetails();
                    contactDetails = HttpContext.Session.getJson<ContactDetails>("CD");
                    patientAppointmentReferral.Name = account.Name;
                    patientAppointmentReferral.DOB = account.DOB;
                    //patientAppointmentReferral.RequestedDate = DateTime.Now.Date;
                    patientAppointmentReferral.County = address.County;
                    patientAppointmentReferral.StreetNumber = address.StreetNumber;
                    patientAppointmentReferral.StreetName = address.StreetName;
                    patientAppointmentReferral.Region = address.Region;
                    patientAppointmentReferral.CurrentDate = DateTime.Now;
                    patientAppointmentReferral.Postcode = address.Postcode;
                    patientAppointmentReferral.Symptoms = "Sore Head, Pains in stomach";
                    patientAppointmentReferral.Email = contactDetails.Email;
                    patientAppointmentReferral.HomePhone = contactDetails.HomePhone;
                    patientAppointmentReferral.WorkPhone = contactDetails.WorkPhone;
                    patientAppointmentReferral.MobilePhone = contactDetails.MobilePhone;
                    patientAppointmentReferral.NextOfKin = contactDetails.NextOfKin;
                    return View("CreateAppointment", patientAppointmentReferral);
                }
            }
            catch (Exception ex)
            {
                patientAppointmentReferral.Name = "Julie Doe";
                patientAppointmentReferral.DOB = new DateTime(1980, 6, 25, 0, 0,0);
                //patientAppointmentReferral.RequestedDate = DateTime.Now.Date;
                patientAppointmentReferral.County = "Lanarkshire";
                patientAppointmentReferral.StreetNumber = 0;
                patientAppointmentReferral.StreetName = "Main Road";
                patientAppointmentReferral.Region = "Strathclyde";
                patientAppointmentReferral.Symptoms = "Sore Head, Pains in Stomach";
                patientAppointmentReferral.CurrentDate = DateTime.Now;
                patientAppointmentReferral.Postcode = "G01 145";
                patientAppointmentReferral.Email = "Ju1ieDoe@example.com";
                patientAppointmentReferral.HomePhone = "0141 123 4567";
                patientAppointmentReferral.WorkPhone = "0123 567 8910";
                patientAppointmentReferral.MobilePhone = "01234 567890";
                patientAppointmentReferral.NextOfKin = "John Doe";
                return View("CreateAppointment", patientAppointmentReferral);// return View();
            }
        }

        //this creates the appointment for the user if the model is valid
        [HttpPost]
        public IActionResult CreatingAppointment(PatientAppointmentReferral PAR, string RequestedDate)
        {
            List<string> newList = new List<string>();
            //this preloads the data for the appointment page
            ViewBag.Dates = newList;
            PAR.Decision = "Booked";
            PAR.RequestedTime = RequestedDate.Substring(11); //PAR.RequestedDate.TimeOfDay.ToString();
            if(PAR.RequestedDate.Date >= DateTime.Now.Date && (PAR.RequestedDate.DayOfWeek.ToString() != "Saturday" || PAR.RequestedDate.DayOfWeek.ToString() != "saturday") || (PAR.RequestedDate.DayOfWeek.ToString() != "Sunday") || (PAR.RequestedDate.DayOfWeek.ToString() != "sunday"))
            {
                if(ModelState.IsValid)
                {
                    Appointment appointment = new Appointment();
                    appointment.AppointmentDate = DateTime.Parse(RequestedDate);
                    //appointment.AppointmentTime = PAR.RequestedTime;
                    appointment.AppointmentTime = RequestedDate.Substring(11);
                    appointment.AppointmentType = "Doctor's Surgery";
                    appointment.County = PAR.County;
                    appointment.CurrentDate = PAR.CurrentDate;
                    appointment.Diagnosis = "";
                    appointment.DOB = PAR.DOB;
                    appointment.PatientFullName = PAR.Name;
                    appointment.Postcode = PAR.Postcode;
                    appointment.Region = PAR.Region;
                    appointment.StreetName = PAR.StreetName;
                    appointment.StreetNumber = PAR.StreetNumber;
                    appointment.Symptoms = PAR.Symptoms;
                    appointment.AppointmentMedicalProfessional = PAR.MedicalPersonnel;

                    //this checks if users are Medical personnel or not
                    if (HttpContext.Session.GetString("Name") != "" && (HttpContext.Session.GetString("Type") == "Doctor" || HttpContext.Session.GetString("Type") == "Nurse"))
                    {
                        try
                        {
                            //this checks if usersis already in the database and adds the appointment to the appropriate database table and displays the appropriate message
                            Account accounts = AccountRepository.Accounts.FirstOrDefault(a => a.Name == appointment.PatientFullName);
                            if (accounts == null)
                            {
                                PARrepository.CreatePAR(PAR);
                                TempData["Message"] = "Appointment Created";
                            }
                            else
                            {
                                //Account account = AccountRepository.Accounts.FirstOrDefault(a => a.Name == appointment.PatientFullName);
                                appointment.UserReferralID = accounts.ID;
                                AppointmentRepository.CreateAppointment(appointment);
                                TempData["Message"] = "Appointment Created";
                            }
                        }
                        catch
                        {
                            //this adds a valid appointment as a guest appointment
                            PARrepository.CreatePAR(PAR);
                        }
                        //this returns the user to the practitioners index view
                        return RedirectToAction("Index", "Practitioners");
                    }
                    else if((HttpContext.Session.GetString("Name") != "" || HttpContext.Session.GetString("Name") != null) && HttpContext.Session.GetString("Type") == "Patient")
                    {
                        //this adds a logged in user's appointment as a logged in user appointment, displays the apppropriate message and redirects the user to their respected homepage
                        Account account = HttpContext.Session.getJson<Account>("Account");
                        appointment.UserReferralID = account.ID;
                        AppointmentRepository.CreateAppointment(appointment);
                        TempData["Message"] = "Appointment Created";
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        //this adds a guest user's appointment as a guest appointment, displays the apppropriate message and redirects the user to their respected homepage
                        PARrepository.CreatePAR(PAR);
                        TempData["Message"] = "Appointment Created";
                        return RedirectToAction("Index", "Appointment");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {

                return View();
            }
        }

        //this allowed user to search for appointments 
        /*[HttpGet]
        public IActionResult Search()
        {
            if (HttpContext.Session.GetString("Type") == "Doctor" || HttpContext.Session.GetString("Type") == "Nurse" || HttpContext.Session.GetString("Type") == "Receptionist")
            {
                //ViewBag.Appointments = 
                return View();
            }
            else if (HttpContext.Session.GetString("Type") == "Patient")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Appointment");
            }
        }*/

        //[HttpGet]
        //public IActionResult Search()
        //{
        //    Account account = HttpContext.Session.getJson<Account>("Account");
        //    return View(PARrepository.PAR.Where(p => p.Name == account.Name).Where(p => p.DOB == account.DOB));
        //}

        //this displays the latest appointments for users if there is any
        [HttpPost]
        public IActionResult Search(string Name, DateTime DOB)
        {
            //this makes sure that user have a character value of at least 3
            Account account = HttpContext.Session.getJson<Account>("Account");
            if (Name.Length >= 3 && DOB <= DateTime.Now)
            {
                //this checks if the account is valid
                if (account != null)
                {
                    try
                    {
                        //this redirects the user to appointment page
                        if (AppointmentRepository.Appointment.Where(a => a.DOB == DOB).Where(a => a.PatientFullName == Name).Where(a => a.AppointmentDate >= DateTime.Now).Where(a => double.Parse(a.AppointmentTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            //this displays the users latest appointments
                            return View(AppointmentRepository.Appointment.Where(a => a.DOB == DOB).Where(a => a.PatientFullName == Name).Where(a => a.AppointmentDate >= DateTime.Now).Where(a => double.Parse(a.AppointmentTime) >= double.Parse(DateTime.Now.ToShortTimeString())));
                        }

                    }
                    catch
                    {
                        //this redirects the user to appointment page
                        if (PARrepository.PAR.Where(a => a.DOB == DOB).Where(a => a.Name == Name).Where(a => a.RequestedDate >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            //this displays the users latest appointments
                            return View(PARrepository.PAR.Where(a => a.Name == account.Name).Where(a => a.RequestedDate >= DateTime.Now).Where(p => p.DOB == DOB).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())));//);//.Where(a => a.DOB == account.DOB).Where(a => a.CurrentDate >= DateTime.Now.Date));
                        }                                                                                                                                                                                                                                 //return RedirectToAction("Index", "Patient");
                    }
                }
                //this checks if users are Doctors or nurses
                else if (HttpContext.Session.GetString("Type") != null && HttpContext.Session.GetString("Type") != "Patient")
                {
                    try
                    {
                        //this redirects the medical personnels to appointment page
                        if (PARrepository.PAR.Where(a => a.DOB == DOB).Where(a => a.Name == Name).Where(a => a.RequestedDate >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            //this displays the medical personnels latest appointments
                            return View(PARrepository.PAR.Where(a => a.MedicalPersonnel == HttpContext.Session.GetString("Name")).Where(a => a.RequestedDate/*.Date*/ >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())));/*.Date));*/
                        }
                    }
                    catch
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    try
                    {
                        //this redirects the user to appointment page
                        if (PARrepository.PAR.Where(a => a.DOB == DOB).Where(a => a.Name == Name).Where(a => a.RequestedDate >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            //this displays the users latest appointments
                            return View(PARrepository.PAR.Where(a => a.Name == Name).Where(a => a.DOB == DOB).Where(a => a.RequestedDate >= DateTime.Now));//.Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())));//.Date));
                        }
                    }
                    catch
                    {
                        //this redirects the user to the home page
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                //this redirects the user to the home page
                return RedirectToAction("Index", "Home");
            }
        }


        //this displays the appointment to be edited
        [HttpGet]
        public IActionResult EditAppointment(int id)
        {
            //this tries to assign the guest appointment from the database to a new PatientAppointmentReferral model called par
            PatientAppointmentReferral par = PARrepository.PAR.FirstOrDefault(p => p.PatientApplicationReferralID == id);
            if(par != null )
            {
                //this loads the dates and times available for appointments to be made
                ViewBag.Dates = timesRepository.ViewbagDates(PARrepository, AppointmentRepository, par.MedicalPersonnel, par.RequestedDate, timesRepository);
                //this passes the model to the EditAppointment view
                return View(PARrepository.PAR.FirstOrDefault(p => p.PatientApplicationReferralID == id));
            }
            else
            {
                //this creates an error message and redirects the user to the default home page
                TempData["Error"] = "Error trying to load appointment";
                return RedirectToAction("Index", "Patient");
            }
        }

        //this edits and saves the appointment
        [HttpPost]
        public IActionResult EditAppointment(int id, PatientAppointmentReferral par)
        {
            //this checks if the model is valid and if not creates an error message and redirects the user to the default home page
            if (ModelState.IsValid)
            {
                //this alters the appointment date and time in the current par variable
                par.RequestedTime = par.RequestedDate.ToString().Substring(11);
                par.PatientApplicationReferralID = id;
                //this updates the guest appointment record's details
                PARrepository.UpdatePAR(par);
                //string ID = "";
                string Name = par.Name;
                DateTime DOB = par.DOB;
                //this redirects the users to the homepage
                return RedirectToAction("Index", "Patient");
                //return RedirectToAction("Search", "Appointment");//, [ID, Name, DOB]);
            }
            else
            {
                TempData["Error"] = "Cannot Edit Appointment";
                return View(par);
            }
        }

        //this displays the appointment to be deleted
        [HttpGet]
        public ViewResult DeleteAppointment(int id)
        {
            //this checks if a user is logged in or not
            if(HttpContext.Session.GetString("Name") != null)
            {
                //this passes logged in appointment to the delete appointment view
                return View(AppointmentRepository.Appointment.FirstOrDefault(app => app.AppointmentID == id));
            }
            else
            {
                //this passes guest appointment to the delete appointment view
                return View(PARrepository.PAR.FirstOrDefault(r => r.PatientApplicationReferralID == id));
            }
        }


        //this deletes the record from the view
        [HttpPost]
        public IActionResult DeleteAppointment(int id, PatientAppointmentReferral par)
        {
            //this assigns the id to the PatientAppointmentReferral model's id 
            par.PatientApplicationReferralID = id;
            //this deletes the guest appointment from the database
            PARrepository.DeletePAR(par);
            //this creates a confirmation message and redirects the user to the appointment homepage
            TempData["Message"] = "Appointment Deleted";
            return RedirectToAction("Index", "Appointment");
        }
    }
}