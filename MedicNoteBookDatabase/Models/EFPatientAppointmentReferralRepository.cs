//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFPatientAppointmentReferralRepository : IPatientAppointmentReferralRepository
    {
        private ApplicationDBContext context;
        public EFPatientAppointmentReferralRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        //public IEnumerable<PatientAppointmentReferral> PAR => context.PatientAppointmentReferral;

        public IQueryable<PatientAppointmentReferral> PAR => context.PatientAppointmentReferral;


        public void CreatePAR(PatientAppointmentReferral par)
        {
            ////PAR.Contains(par);
            //if(context.PatientAppointmentReferral.Contains(par) == false && PAR.Contains(par) == false)
            //{
            //par.CurrentDate = DateTime.Now;
            //par.Decision = "Not Available";
            context.PatientAppointmentReferral.Add(par);

            // PAR.Append(par);
            //}
            context.SaveChanges();
        }
        /* early version of updatePAR
        public void SavePAR(PatientAppointmentReferral par)
        {
            //if (context.PatientAppointmentReferral.Contains(par))
            //{
            //    context.PatientAppointmentReferral.Add(par);
            //}
        }
        */

            //this updates edited Guest Appointments
        public void UpdatePAR(PatientAppointmentReferral par)
        {
            PatientAppointmentReferral PARUpdate = context.PatientAppointmentReferral.FirstOrDefault(r => r.PatientApplicationReferralID == par.PatientApplicationReferralID);
            PARUpdate.Name = par.Name;
            PARUpdate.RequestedDate = par.RequestedDate;
            PARUpdate.RequestedTime = par.RequestedTime;
            PARUpdate.Region = par.Region;
            PARUpdate.Symptoms = par.Symptoms;
            PARUpdate.DOB = par.DOB;
            PARUpdate.Postcode = par.Postcode;
            PARUpdate.Decision = par.Decision;
            PARUpdate.StreetNumber = par.StreetNumber;
            PARUpdate.StreetName = par.StreetName;
            PARUpdate.County = par.County;
            PARUpdate.CurrentDate = par.CurrentDate;

            context.SaveChanges();
        }

        //this deletes the record of the guest user's appointment
        public void DeletePAR(PatientAppointmentReferral par)
        {
            if(context.PatientAppointmentReferral.Where(p => p.PatientApplicationReferralID == par.PatientApplicationReferralID) != null)//id) != null) 
            {
                context.PatientAppointmentReferral.Remove(par);
                context.SaveChanges();
            }
        }

        //this returns a list of patientAppointmentReferral Models in the database
        public List<PatientAppointmentReferral> ViewBagPAR(string MedicalProfessional)
        {
            List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();
            PAR = context.PatientAppointmentReferral.Where(p => p.RequestedDate >= DateTime.Now).Where(p => p.MedicalPersonnel == MedicalProfessional).ToList();
            return PAR;
        }
    }
}
