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
        private IDataProtector _protector;

        public AppointmentController(IDataProtectionProvider provider , IPatientAppointmentReferralRepository PARrepo, IAccountRepository accountRepo, IAddressRepository AddressRepo, IContactDetailsRepository CDRepo, IAppointmentRepository appointmentRepo, ITimesRepository timesRepo)
        {
            PARrepository = PARrepo;
            AccountRepository = accountRepo;
            AddressRepository = AddressRepo;
            CDRepository = CDRepo;
            AppointmentRepository = appointmentRepo;
            timesRepository = timesRepo;
            _protector = provider.CreateProtector("c90bab3c-8f97 -461b-af78-16fcfc574edb");//GetType().FullName);
            //_protector = provider.CreateProtector(GetType().FullName);
            //_protector = provider.CreateProtector("key-d3431142-2392-4951-a994-125bf74c8d2b");
        }

        public ViewResult Index()
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

        //[HttpGet]
        [HttpPost]
        public /*IActionResult*/ ViewResult CreateAppointment(string MedicalPersonnel)//ViewResult> CreateAppointment()
        {
            //try
            ////{
            //    Account account = new Account();
            //    Address address = new Address();
            //    ContactDetails contactDetails = new ContactDetails();
                PatientAppointmentReferral patientAppointmentReferral = new PatientAppointmentReferral();
            patientAppointmentReferral.MedicalPersonnel = MedicalPersonnel;
            //List<string> accounts = new List<string>();
            //    List<string> times = new List<string>();

            //    account = HttpContext.Session.getJson<Account>("Account");
            //    address = HttpContext.Session.getJson<Address>("Address");
            //    contactDetails = HttpContext.Session.getJson<ContactDetails>("CD");
            //    //if (HttpContext.Session.GetString("Name") == null)
            //    //{
            //    //    List<Account> doctors = new List<Account>();
            //    //    doctors = AccountRepository.Accounts.Where(a => a.RoleID == 3).ToList();
            //    //    accounts.Add("None");
            //    //    foreach (var item in doctors)
            //    //    {
            //    //        //accounts.Add(item.MedicalPersonnel.ToString());
            //    //        accounts.Add(item.Name.ToString());
            //    //    }
            //    //    ViewBag.doctorNo = accounts.Count();
            //    //    ViewBag.doctors = accounts;
            //    //}
            //    //else
            //    //{
            //    //    accounts.Add(account.MedicalPersonnel);
            //    //}
            //    //ViewBag.doctors = doctors;

            //        List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();
            //        List<Appointment> appointment = new List<Appointment>();
            //        List<int> dateValues = new List<int>();
            //        PAR = PARrepository.PAR.Where(a => a.RequestedDate >= DateTime.Now.Date).ToList();
            //        appointment = AppointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now.Date).ToList();
            //        ViewBag.PAR = PAR;
            //        ViewBag.Appointment = appointment;


            ////this attempts to fill the 
            ////try
            ////{
            ////    patientAppointmentReferral.MedicalPersonnel = account.MedicalPersonnel;
            ////}
            ////catch
            ////{
            //    patientAppointmentReferral.MedicalPersonnel = MedicalPersonnel;
            ////}

            //List<string> dateTimes = new List<string>();
            //DateTime days = DateTime.Now;
            //    //List<Times> dateTimes = new List<Times>();
            //    for (int i = 0; i <= 31; i++)
            //        {
            //            DateTime day = DateTime.Now.AddDays(i);
            //            //TimeSpan date = day.TimeOfDay;
            //            DateTime dates;
            //    //day.TimeOfDay.Subtract(date);
            //    //if (account.MedicalPersonnel.Length > 0 || patientAppointmentReferral.MedicalPersonnel.Length > 0)
            //            if(MedicalPersonnel.Length > 0)
            //            {
            //                Appointment appointmentDay = appointment.FirstOrDefault(d => d.AppointmentDate == day);
            //                PatientAppointmentReferral patientAppointmentDay = PAR.FirstOrDefault(p => p.RequestedDate == day);
            //                for(int t = 1; t <= timesRepository.Times.Count(); t++)
            //                {
            //                    Times time = timesRepository.Times.FirstOrDefault(v => v.ID == t);
            //                    try
            //                    {
            //                        dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time/*.ToString()*/.Substring(0, 2)), int.Parse(time.Time/*.ToString()*/.Substring(3)), 0);

            //                        //if (day.DayOfWeek != DayOfWeek.Saturday || day.DayOfWeek != DayOfWeek.Sunday /*&&*/ || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != account.MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != account.MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
            //                        //{
            //                        if (/*dates.TimeOfDay >= day.TimeOfDay *//*|| */dates/*Date.*/.DayOfWeek != DayOfWeek.Saturday || dates/*Date.*/.DayOfWeek != DayOfWeek.Sunday /*&&*/ || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel/*account.MedicalPersonnel*/ && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel/*account.MedicalPersonnel*/ && patientAppointmentDay.RequestedTime != time.Time))
            //                        {
            //                        //if (day < dates && ((-day.TimeOfDay + dates.TimeOfDay) >= TimeSpan.FromMinutes(45)))
            //                        if(dates > days)//>
            //                        {
            //                            //Times listTime = new Times();
            //                            //time.ID = i;
            //                            //time.Time = "";
            //                            //time.DateTime = dates.ToString();
            //                            //dateTimes.Add(time);
            //                                dateTimes.Add(dates.ToString());/*Date.*/
            //                         }//day.AddHours(Double.Parse(time.Time.ToString().Substring(0,2)));
            //                            //day.AddMinutes(Double.Parse(time.Time.ToString().Substring(3)));
            //                            //dateTimes.Add(day.ToString());
            //                        }
            //                    }
            //                    catch
            //                    {
            //                        try
            //                        {
            //                            dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(2)), 0);
            //                            //if (day.DayOfWeek != DayOfWeek.Saturday || day.DayOfWeek != DayOfWeek.Sunday /*&&*/ || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != account.MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != account.MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
            //                            //{
            //                            if (/*dates.TimeOfDay <= day.TimeOfDay || */(dates/*Date.*/.DayOfWeek != DayOfWeek.Saturday || dates/*Date.*/.DayOfWeek != DayOfWeek.Sunday) && /*||*/ (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != account.MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != account.MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
            //                            {
            //                                if (dates > days)//>
            //                                {
            //                                    //Times listTime = new Times();
            //                                    //listTime.ID = i;
            //                                    //listTime.DateTime = dates.ToString();
            //                                    //dateTimes.Add(time);
            //                                    dateTimes.Add(dates.ToString());/*Date.*/
            //                                }
            //                            }
            //                            //day.AddHours(Double.Parse(time.Time.ToString().Substring(0, 1)));
            //                            //day.AddMinutes(Double.Parse(time.Time.ToString().Substring(2)));
            //                            //dateTimes.Add(day.ToString());
            //                        }
            //                        catch
            //                        {
            //                            //try
            //                            //{
            //                            dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(3)), 0);
            //                            //if (day.DayOfWeek != DayOfWeek.Saturday || day.DayOfWeek != DayOfWeek.Sunday /*&&*/ || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != account.MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != account.MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
            //                            //{
            //                            if (/*dates.TimeOfDay >= day.TimeOfDay || */dates/*Date.*/.DayOfWeek != DayOfWeek.Saturday || dates/*Date.*/.DayOfWeek != DayOfWeek.Sunday /*&&*/ || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != account.MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != account.MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
            //                            {
            //                                if (dates > days)//>
            //                                {
            //                                    //Times listTime = new Times();
            //                                    //listTime.ID = i;
            //                                    //listTime.DateTime = dates.ToString();
            //                                    //dateTimes.Add(time);
            //                                    dateTimes.Add(dates.ToString());/*Date.*/
            //                                }
            //                            }
            //                            //}
            //                            //catch
            //                            //{
            //                            //DateTime dates = new DateTime(int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(day.Date.Year.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(3)), 00);
            //                            //dateTimes.Add(dates.ToString());
            //                            //}
            //                        }
            //                        //day.AddHours(Double.Parse(time.Time.ToString().Substring(0, 1)));
            //                        //day.AddMinutes(Double.Parse(time.Time.ToString().Substring(2)));
            //                        //dateTimes.Add(day.ToString());
            //                    }
            //                }
            //            }
            //        ViewBag.Dates = dateTimes;
            List<string> dates = timesRepository.ViewbagDates(PARrepository, AppointmentRepository, patientAppointmentReferral.MedicalPersonnel, DateTime.Now, timesRepository);
            ViewBag.Dates = dates;
            //else
            //{ 
            //    //DateTime day = DateTime.Now.AddDays(i);
            //    if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        //dateTimes.Add(day.Date.ToShortDateString());
            //    }
            //    ViewBag.Dates = dateTimes;
            //}
            //}


            //int timeID = timesRepository.Times.Count();

            //foreach(var item in accounts)
            //{
            //    foreach(var date in dateTimes)
            //    {
            //        for(int i = 1; i <= timeID; i++)
            //        {
            //            Times time = timesRepository.Times.FirstOrDefault(t => t.ID == i);
            //            if(PAR.Where(p => p.RequestedTime == time.Time).Where(d => d.MedicalPersonnel == item).Where(d => d.RequestedDate == DateTime.Parse(date)) == null)
            //            {
            //                times.Add(time.Time);
            //            }
            //            //this checks if the item if the doctor has an apopointment in the 
            //            if(appointment.Where(a => a.AppointmentTime == time.Time).Where(d => d.AppointmentMedicalProfessional == item).Where(d => d.AppointmentDate == DateTime.Parse(date)) == null && times.Contains(time.Time) == false)
            //            {
            //                times.Add(time.Time);
            //            }
            //        }
            //        dateValues.Add(times.Count());
            //    }
            //}
            //ViewBag.values = dateValues;
            //for (int i = 1; i <= timeID; i++)
            //{
            //    Times time = timesRepository.Times.FirstOrDefault(t => t.ID == i);
            //    times.Add(time.Time);
            //}
            //ViewBag.times = times;
            ////}
            //    //this attempts to fill the 
            //try
            //{ 
            //    patientAppointmentReferral.MedicalPersonnel = account.MedicalPersonnel;
            //}
            //catch
            //{
            //    patientAppointmentReferral.MedicalPersonnel = patientAppointmentReferral.MedicalPersonnel;
            //}
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

        [HttpPost]
        public IActionResult CreatingAppointment(PatientAppointmentReferral PAR, string RequestedDate)//, ContactDetails contactDetails, Address address, Account account)
        {
            List<string> newList = new List<string>();
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

                    if (HttpContext.Session.GetString("Name") != "" && (HttpContext.Session.GetString("Type") == "Doctor" || HttpContext.Session.GetString("Type") == "Nurse"))
                    {
                        try
                        {
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
                            PARrepository.CreatePAR(PAR);
                        }
                        return RedirectToAction("Index", "Practitioners");
                    }
                    else if((HttpContext.Session.GetString("Name") != "" || HttpContext.Session.GetString("Name") != null) && HttpContext.Session.GetString("Type") == "Patient")
                    {
                        Account account = HttpContext.Session.getJson<Account>("Account");
                        appointment.UserReferralID = account.ID;
                        AppointmentRepository.CreateAppointment(appointment);
                        TempData["Message"] = "Appointment Created";
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
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

        [HttpGet]
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
        }
        //[HttpGet]
        //public IActionResult Search()
        //{
        //    Account account = HttpContext.Session.getJson<Account>("Account");
        //    return View(PARrepository.PAR.Where(p => p.Name == account.Name).Where(p => p.DOB == account.DOB));
        //}

        [HttpPost]
        public IActionResult Search(/*string ID, */string Name, DateTime DOB)
        {
            Account account = HttpContext.Session.getJson<Account>("Account");

            if (Name.Length >= 3 && DOB <= DateTime.Now)
            {
                if (account != null)
                {
                    try
                    {
                        if (AppointmentRepository.Appointment.Where(a => a.DOB == DOB).Where(a => a.PatientFullName == Name).Where(a => a.AppointmentDate >= DateTime.Now).Where(a => double.Parse(a.AppointmentTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            return View(AppointmentRepository.Appointment.Where(a => a.DOB == DOB).Where(a => a.PatientFullName == Name).Where(a => a.AppointmentDate >= DateTime.Now).Where(a => double.Parse(a.AppointmentTime) >= double.Parse(DateTime.Now.ToShortTimeString())));
                        }

                    }
                    catch
                    {
                        if (PARrepository.PAR.Where(a => a.DOB == DOB).Where(a => a.Name == Name).Where(a => a.RequestedDate >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            return View(PARrepository.PAR.Where(a => a.Name == account.Name).Where(a => a.RequestedDate >= DateTime.Now).Where(p => p.DOB == DOB).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())));//);//.Where(a => a.DOB == account.DOB).Where(a => a.CurrentDate >= DateTime.Now.Date));
                        }                                                                                                                                                                                                                                 //return RedirectToAction("Index", "Patient");
                    }
                }
                else if (HttpContext.Session.GetString("Type") != null && HttpContext.Session.GetString("Type") != "Patient")
                {
                    try
                    {
                        if (PARrepository.PAR.Where(a => a.DOB == DOB).Where(a => a.Name == Name).Where(a => a.RequestedDate >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
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
                        if (PARrepository.PAR.Where(a => a.DOB == DOB).Where(a => a.Name == Name).Where(a => a.RequestedDate >= DateTime.Now).Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())) == null)
                        {
                            return RedirectToAction("Index", "Appointment");
                        }
                        else
                        {
                            return View(PARrepository.PAR.Where(a => a.Name == Name).Where(a => a.DOB == DOB).Where(a => a.RequestedDate >= DateTime.Now));//.Where(a => double.Parse(a.RequestedTime) >= double.Parse(DateTime.Now.ToShortTimeString())));//.Date));
                        }
                    }
                    catch
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult EditAppointment(int id)
        {
            PatientAppointmentReferral par = PARrepository.PAR.FirstOrDefault(p => p.PatientApplicationReferralID == id);
            if(par != null )// || AppointmentRepository.Appointment.FirstOrDefault(a => a.AppointmentID == id) != null)
            {
                ViewBag.Dates = timesRepository.ViewbagDates(PARrepository, AppointmentRepository, par.MedicalPersonnel, par.RequestedDate, timesRepository);
                return View(PARrepository.PAR.FirstOrDefault(p => p.PatientApplicationReferralID == id));
            }
            else
            {
                TempData["Error"] = "Error trying to load appointment";
                return RedirectToAction("Index", "Patient");
            }
        }

        [HttpPost]
        public IActionResult EditAppointment(int id, PatientAppointmentReferral par)
        {
            if(ModelState.IsValid)
            {
                par.RequestedTime = par.RequestedDate.ToString().Substring(11);
                par.PatientApplicationReferralID = id;
                PARrepository.UpdatePAR(par);/*id, */
                //string ID = "";
                string Name = par.Name;
                DateTime DOB = par.DOB;
                //Search(Name, DOB);/*ID,*/
                return RedirectToAction("Index", "Patient");
                //return RedirectToAction("Search", "Appointment");//, [ID, Name, DOB]);
            }
            else
            {
                TempData["Error"] = "Cannot Edit Appointment";
                return View(par);
            }
        }

        [HttpGet]
        public ViewResult DeleteAppointment(int id)
        {
            if(HttpContext.Session.GetString("Name") != null)
            {
                return View(AppointmentRepository.Appointment.FirstOrDefault(app => app.AppointmentID == id));
            }
            else
            {
                return View(PARrepository.PAR.FirstOrDefault(r => r.PatientApplicationReferralID == id));
            }
        }


        //this is used for users
        [HttpPost]
        public IActionResult DeleteAppointment(int id, PatientAppointmentReferral par)
        {
            par.PatientApplicationReferralID = id;
            PARrepository.DeletePAR(par);/*id, */

            TempData["Message"] = "Appointment Deleted";
            return /*View*/ RedirectToAction("Index", "Appointment");
        }

        //private class Time
        //{
        //}
    }
}