using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IAppointmentRepository
    {
        IQueryable<Appointment> Appointment { get; }

        void SaveAppointment(int id, Appointment appointment);

        void CreateAppointment(Appointment appointment);

        void DeleteAppointment(int id, Appointment appointment);

        List<Appointment> ViewBagAppointments(string MedicalProfessional);
        
    }
}