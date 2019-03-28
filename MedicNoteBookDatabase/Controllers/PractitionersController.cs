using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicNoteBookDatabase.Infrastructure;
using MedicNoteBookDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;


namespace MedicNoteBookDatabase.Controllers
{
    public class PractitionersController : Controller
    {
        private IAppointmentRepository appointmentRepository;
        private IPatientAppointmentReferralRepository patientAppointmentReferralRepository;
        private IDataProtector _protector;
        
        public PractitionersController(IDataProtectionProvider provider, IAppointmentRepository appointmentRepo, IPatientAppointmentReferralRepository PARrepo)
        {
            appointmentRepository = appointmentRepo;
            patientAppointmentReferralRepository = PARrepo;
            _protector = provider.CreateProtector("c90bab3c-8f97 -461b-af78-16fcfc574edb");//GetType().FullName);
            //_protector = provider.CreateProtector(GetType().FullName);
            //_protector = provider.CreateProtector("key-d3431142-2392-4951-a994-125bf74c8d2b");
        }

        public IActionResult Index()
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if(account != null)
            {
                if(HttpContext.Session.GetString("Type") != "Patient" || HttpContext.Session.GetString("Type") != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult SearchAll()
        {
            Account account = new Account();
            account = HttpContext.Session.getJson<Account>("Account");
            if(account != null)
            { 
                if(account.RoleID == 3 || account.RoleID == 4 || account.RoleID == 6)
                {
                    List<Appointment> appointments = new List<Appointment>();
                    appointments = appointmentRepository.ViewBagAppointments(account.Name);//.Appointment.Where(a => a.AppointmentDate >= DateTime.Now).Where(a => a.AppointmentMedicalProfessional == account.Name).ToList();
                    List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();
                    PAR = patientAppointmentReferralRepository.ViewBagPAR(account.Name);//.PAR.Where(p => p.RequestedDate >= DateTime.Now).Where(p => p.MedicalPersonnel == account.Name).ToList();

                    if(appointments.Count > 0)
                    {
                        ViewBag.Appointments = appointments;
                        if(PAR.Count > 0)
                        {
                            ViewBag.PAR = PAR;
                            return View();
                        }
                        else
                        {
                            PAR = new List<PatientAppointmentReferral>();
                            ViewBag.PAR = PAR;
                            return View();
                        }
                    }
                    else if(PAR.Count > 0)
                    {
                        ViewBag.PAR = PAR;
                        if(appointments.Count > 0)
                        {
                            ViewBag.Appointments = appointments;
                            return View();
                        }
                        else
                        {
                            appointments = new List<Appointment>();
                            ViewBag.Appointments = appointments;
                            return View();
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Practitioners");
                    }

                }
                else if(account.RoleID == 7)
                {
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    return RedirectToAction("Index", "Appointment");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //public ViewResult AppointmentConfirm(int userId)
        //{
        //    Appointment appointment = new Appointment();
        //    PatientAppointmentReferral par = patientAppointmentReferralRepository.PAR;
        //    return View();
        //}
    }
}