//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IAppointmentRepository
    {
        IQueryable<Appointment> Appointment { get; }

        //this creates an instance of a method that allows the editing of appointments
        void SaveAppointment(int id, Appointment appointment);

        //this creates an instance of a method that allows the creation of appointments
        void CreateAppointment(Appointment appointment);

        //this creates an instance of a method that allows the deleting of appointments
        void DeleteAppointment(int id, Appointment appointment);

        List<Appointment> ViewBagAppointments(string MedicalProfessional);
        
    }
}