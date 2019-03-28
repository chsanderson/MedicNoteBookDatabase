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

        public IActionResult Index()/*ViewResult */
        {
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

        [HttpPost]
        public IActionResult Appointments()//(int ID)//Account account)
        {
            Account account = new Account(); 
            account = HttpContext.Session.getJson<Account>("Account");
            if (account == null)
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            {
                //if (HttpContext.Session.GetString("Name") != null || HttpContext.Session.GetString("Name") != "")
            //{
                //Account account = HttpContext.Session.getJson<Account>("Account");
                ////Appointment appointment = ;
                //ViewBag.Dates = timesRepository.ViewbagDates(parRepository, appointmentRepository, account.MedicalPersonnel,timesRepository);
                return View(appointmentRepository.Appointment.Where(app => app.UserReferralID == account.ID));//ID));// account.ID));
            }
            //else
            //{
            //    return RedirectToAction("Index", "Appointment");
            //}
        }

        public /*ViewResult*/ IActionResult PatientAppointmentReferral(/*int ID, */string Name, DateTime DOB)//Account account)
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if(account == null)
            {
                return RedirectToAction("Index", "Appointment");
            }
            else
            { 
                if(HttpContext.Session.GetString("Type") == "Patient")
                {
                    return View(appointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now).Where(a => a.DOB == DOB).Where(a => a.PatientFullName == Name));//.Where(a => a.Diagnosis == null));
                }
                else
                {
                    return View(appointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now).Where(a => a.AppointmentMedicalProfessional == HttpContext.Session.GetString("Name")));
                }
            }
            //return View(parRepository.PAR.Where(a => a.Name == account.Name).Where(a => a.DOB == account.DOB));
        }

        [HttpGet]
        public /*ViewResult*/ IActionResult EditAppointment(int id)
        {
            //if (HttpContext.Session.GetString("Name") != null)

            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if (account != null)
            {
                Appointment appointment = appointmentRepository.Appointment.FirstOrDefault(a => a.AppointmentID == id);
                //if(HttpContext.Session.GetString("Type") == "Doctor" || HttpContext.Session.GetString("Type") == "Nurse")
                //{
                //    List<string> dates = timesRepository.ViewbagDates(parRepository, );
                //}
                //else
                //{
                    List<string> dates = timesRepository.ViewbagDates(parRepository, appointmentRepository, appointment.AppointmentMedicalProfessional, appointment.AppointmentDate, timesRepository);
                //}
                ViewBag.Dates = dates;
                return View(appointmentRepository.Appointment.FirstOrDefault(app => app.AppointmentID == id));
            }
            else
            {
                return RedirectToAction("Index", "Appointment");
            }
        }

        [HttpPost]
        public IActionResult EditAppointment(int id, Appointment appointment)
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if(account != null)
            {
                if (ModelState.IsValid)
                {
                    appointmentRepository.SaveAppointment(id, appointment);
                    return RedirectToAction("Index", "Patient");
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

        [HttpGet]
        public /*ViewResult*/ IActionResult DeleteAppointment(int id)
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

        //this is used for users
        [HttpPost]
        public IActionResult DeleteAppointment(int id, Appointment app)
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            appointmentRepository.DeleteAppointment(id, app);
            return RedirectToAction("Index", "Patient");
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}