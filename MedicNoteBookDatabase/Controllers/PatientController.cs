//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicNoteBookDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using MedicNoteBookDatabase.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;

namespace MedicNoteBookDatabase.Controllers
{
    public class PatientController : Controller
    {
        private IAccountRepository accountRepository;
        private IContactDetailsRepository contactDetailsRepository;
        private IAddressRepository addressRepository;
        private IPatientAppointmentReferralRepository parRepository;
        private IAppointmentRepository appointmentRepository;
        private ITimesRepository timesRepository;
        private IDataProtector _protector;
        private IDataProtectionService protectionService;

        public PatientController(/*IDataProtectionProvider providerIDataProtectionService protectionServices,*/ IAccountRepository accountrepo, IContactDetailsRepository contactDetailsRepo, IAddressRepository addressRepo, IPatientAppointmentReferralRepository IPAR, IAppointmentRepository appointmentRepo, ITimesRepository timesRepo)
        {
            appointmentRepository = appointmentRepo;
            accountRepository = accountrepo;
            contactDetailsRepository = contactDetailsRepo;
            addressRepository = addressRepo;
            parRepository = IPAR;
            timesRepository = timesRepo;
            //protectionService = protectionServices;
            //_protector = provider.CreateProtector("c90bab3c-8f97 -461b-af78-16fcfc574edb");//GetType().FullName);
            //_protector = provider.CreateProtector(GetType().FullName);
            //_protector = provider.CreateProtector("key-d3431142-2392-4951-a994-125bf74c8d2b");
        }
        //this displays the patient controllers index view
        public IActionResult Index()/*ViewResult */
        {
            //this checks if an account session is valid or not and redirects the user to the appropriate view
            Account account = HttpContext.Session.getJson<Account>("Account");
            if(account != null)
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Index", "Appointment");
            }
        }

        //[]
        //public ViewResult Index(int[] ids)
        //{
        //    return View(ids);
        //}

        //[Authorize]
        //public ViewResult MedicalHistory()
        //{
        //[HttpGet]
        //public IActionResult MedicalHistory()
        //{
        //    Account account = HttpContext.Session.getJson<Account>("Account");
        //    if (account != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //[Authorize]

        //[HttpPost]
        //public IActionResult MedicalHistory(string appointmentType)
        //{
        //    //if (HttpContext.Session.GetString("Account") != "" && HttpContext.Session.GetString("Address") != "" && HttpContext.Session.GetString("CD") != "")
        //    //{
        //    Account account = HttpContext.Session.getJson<Account>("Account");

        //    if (account != null && appointmentType == "Appointments")
        //    {
        //        Appointments(account);
        //        Account newAccount = new Account();
        //        PatientAppointmentReferral(newAccount);
        //        return View();
        //    }
        //    else if (account != null && appointmentType == "PatientAppointmentReferrals")
        //    {
        //        PatientAppointmentReferral(account);
        //        Account newAccount = new Account();
        //        Appointments(newAccount);
        //        return View();
        //    }
        //    else if(account != null && appointmentType == "All")
        //    {
        //        Appointments(account);
        //        PatientAppointmentReferral(account);
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    //}
        //    //else
        //    //{

        //    //    return RedirectToAction();
        //    //}
        //}
        [HttpGet]
        public IActionResult PatientAppointmentReferral()
        {
            Account account = HttpContext.Session.getJson<Account>("Account");
            if(account == null)
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                return PatientAppointmentReferral(account.Name, account.DOB);
            }
        }

        //this displays the appointment view and passes the IQueryable models to the view if there are any
        [HttpPost]
        public IActionResult Appointments()
        {
            //this checks if the account session is valid and redirectsthe user if not to the appointment index view
            Account account = new Account(); 
            account = HttpContext.Session.getJson<Account>("Account");
            if (account == null)
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                //this passes the passes the IQueryable models to the view if there are any
                return View(appointmentRepository.Appointment.Where(app => app.UserReferralID == account.ID));
            }
        }

        //this displays the latest appointments created by the user as long as they are in the future compared to the time at this precise moment
        public IActionResult PatientAppointmentReferral(string Name, DateTime DOB)
        {

            //this checks if the account session is valid and redirectsthe user if not to the appointment index view
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if(account == null)
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                //this checks if the user is a patient and displays their appointments as long as they are in the future compared to the time at this precise moment
                if (HttpContext.Session.GetString("Type") == "Patient")
                {
                    return View(appointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now).Where(a => a.DOB == DOB).Where(a => a.PatientFullName == Name));//.Where(a => a.Diagnosis == null));
                }
                else
                {
                    //this displays the medicalPersonnels appointments as long as they are in the future compared to the time at this precise moment
                    return View(appointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now).Where(a => a.AppointmentMedicalProfessional == HttpContext.Session.GetString("Name")));
                }
            }
            //
            //return View(parRepository.PAR.Where(a => a.Name == account.Name).Where(a => a.DOB == account.DOB));
        }


        //this allows logged in user to edit their appointment time
        [HttpGet]
        public IActionResult EditAppointment(int id)
        {
            //if (HttpContext.Session.GetString("Name") != null)

            //this checks if the account session is valid and redirectsthe user if not to the appointment index view
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if (account != null)
            {
                Appointment appointment = appointmentRepository.Appointment.FirstOrDefault(a => a.AppointmentID == id);

                //this generates list of dateTime variables converted to string form
                    List<string> dates = timesRepository.ViewbagDates(parRepository, appointmentRepository, appointment.AppointmentMedicalProfessional, appointment.AppointmentDate, timesRepository);
                
                //this assigns the list of strings to a ViewBag.Dates variable which can be accessed from the view
                ViewBag.Dates = dates;
                //this passes the appointment record to the edit appointment view
                return View(appointmentRepository.Appointment.FirstOrDefault(app => app.AppointmentID == id));
            }
            else
            {
                return RedirectToAction("Index", "Appointment");
            }
        }

        //this checks if the model is valid and if it is then the appointment will be added 
        [HttpPost]
        public IActionResult EditAppointment(int id, Appointment appointment)
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if(account != null)
            {
                //this checks if the model is valid 
                if (ModelState.IsValid)
                {
                    //this saves the appointment details
                    appointmentRepository.SaveAppointment(id, appointment);
                    //this checks if the user is a patient or medical personnel and redirects them to the controller
                    if(HttpContext.Session.GetString("Type") == "Patient")
                    {
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Practitioners");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Appointment");
            }
        }

        //this displays the appointment if the appointment is valid 
        [HttpGet]
        public IActionResult DeleteAppointment(int id)
        {
            //if (HttpContext.Session.GetString("Name") != null)
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if (account == null)
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                //List<string> dates = timesRepository.ViewbagDates(parRepository, appointmentRepository, account.MedicalPersonnel, timesRepository);
                //ViewBag.Dates = dates;
                return View(appointmentRepository.Appointment.FirstOrDefault(app => app.AppointmentID == id));
            }
            //else
            //{
            //    return View(parRepository.PAR.FirstOrDefault(r => r.PatientApplicationReferralID == id));
            //}
        }

        //this allows the deletion of appointments 
        [HttpPost]
        public IActionResult DeleteAppointment(int id, Appointment app)
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            //this calls the method that allows the appointment to be deleted
            appointmentRepository.DeleteAppointment(id, app);
            return RedirectToAction("Index", "Patient");
        }
    }
}