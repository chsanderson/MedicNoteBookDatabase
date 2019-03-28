using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicNoteBookDatabase.Models;

namespace MedicNoteBook.Controllers
{
    public class AppointmentController : Controller
    {
        private IPatientAppointmentReferralRepository repository;
        //private ApplicationDBContext repository;

        public AppointmentController(IPatientAppointmentReferralRepository PARrepository)
        //public AppointmentController(ApplicationDBContext PARrepository)
        {
            repository = PARrepository;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CreateAppointment()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateAppointment(PatientAppointmentReferral PAR)
        {
            //IPatientAppointmentReferralRepository IPAR;
            if (ModelState.IsValid)
            {
                //IPAR.createPAR(PAR);
                repository.CreatePAR(/*new */PAR);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //public ViewResult Search(string Name)
        //{
        //    IPatientAppointmentReferralRepository IPAR;
        //    return View(IPAR.PAR);
        //}
        public ViewResult Search() => View(repository.PAR);
    }
}