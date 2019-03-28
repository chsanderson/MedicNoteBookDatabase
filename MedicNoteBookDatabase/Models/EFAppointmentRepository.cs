using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFAppointmentRepository : IAppointmentRepository
    {
        private ApplicationDBContext context;

        public EFAppointmentRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public IQueryable<Appointment> Appointment => context.Appointment;

        public void SaveAppointment(int id, Appointment appointment)
        {
            Appointment editedAppointment = context.Appointment.FirstOrDefault(a => a.AppointmentID == id);
            editedAppointment.AppointmentDate = appointment.AppointmentDate;
            editedAppointment.AppointmentTime = appointment.AppointmentTime;
            editedAppointment.AppointmentMedicalProfessional = appointment.AppointmentMedicalProfessional;
            editedAppointment.AppointmentType = appointment.AppointmentType;
            editedAppointment.County = appointment.County;
            editedAppointment.CurrentDate = appointment.CurrentDate;
            editedAppointment.Diagnosis = appointment.Diagnosis;
            editedAppointment.DOB = appointment.DOB;
            editedAppointment.PatientFullName = appointment.PatientFullName;
            editedAppointment.Postcode = appointment.Postcode;
            editedAppointment.Region = appointment.Region;
            editedAppointment.StreetName = appointment.StreetName;
            editedAppointment.StreetNumber = appointment.StreetNumber;
            editedAppointment.Symptoms = appointment.Symptoms;
            editedAppointment.UserReferralID = appointment.UserReferralID;
            context.SaveChanges();
        }

        public void CreateAppointment(Appointment appointment)
        {
            if(context.Appointment.Contains(appointment) == false)
            {
                context.Appointment.Add(appointment);
                context.SaveChanges();
            }
        }

        public void DeleteAppointment(int id, Appointment appointment)
        {
            Appointment app = context.Appointment.FirstOrDefault(i => i.AppointmentID == id);
            if(app != null)
            {
                context.Appointment.Remove(app);
                context.SaveChanges();
            }
        }

        public List<Appointment> ViewBagAppointments(string MedicalProfessional)
        {
            List<Appointment> appointments = new List<Appointment>();
            appointments = context.Appointment.Where(a => a.AppointmentDate >= DateTime.Now).Where(a => a.AppointmentMedicalProfessional == MedicalProfessional).ToList();
            return appointments;
        }
    }
}
