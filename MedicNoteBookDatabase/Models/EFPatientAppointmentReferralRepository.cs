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

        //public bool CreatePAR(PatientAppointmentReferral par)
        //{
        //    if(par.)
        //    context.PatientAppointmentReferral.Add(par);
        //    context.SaveChanges();
        //    return true;
        //}

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

        public void SavePAR(PatientAppointmentReferral par)
        {
            //if (context.PatientAppointmentReferral.Contains(par))
            //{
            //    context.PatientAppointmentReferral.Add(par);
            //}
        }

        public void UpdatePAR(PatientAppointmentReferral par)
        {
            //    if(context.PatientAppointmentReferral.Find(id) != null)
            //    {context.PatientAppointmentReferral.;
            //context.PatientAppointmentReferral.FirstOrDefault(r => r.PatientApplicationReferralID == id);
            PatientAppointmentReferral PARUpdate = context.PatientAppointmentReferral.FirstOrDefault(r => r.PatientApplicationReferralID == par.PatientApplicationReferralID);//id);
            ////context.Update(par).;
            //PARUpdate.PatientApplicationReferralID = par.PatientApplicationReferralID;
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
               // context.PatientAppointmentReferral.
                //context.PatientAppointmentReferral.Update(id).CurrentValues.;//.up;
        //    }
        }

        public void DeletePAR(/*int id, */PatientAppointmentReferral par)
        {
            if(context.PatientAppointmentReferral.Where(p => p.PatientApplicationReferralID == par.PatientApplicationReferralID) != null)//id) != null) 
            {
                context.PatientAppointmentReferral.Remove(par);
                context.SaveChanges();
                //context.PatientAppointmentReferral.Remove(id).
            }
        }

        public List<PatientAppointmentReferral> ViewBagPAR(string MedicalProfessional)
        {
            List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();
            PAR = context.PatientAppointmentReferral.Where(p => p.RequestedDate >= DateTime.Now).Where(p => p.MedicalPersonnel == MedicalProfessional).ToList();
            return PAR;
        }
    }
}
