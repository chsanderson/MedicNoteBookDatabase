//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface ITimesRepository
    {
        //this creates an instance of a getter with a dataType of Iqueryable using a times object
        IQueryable<Times> Times { get; }

        //this creates an instance of a method that returns a list of strings. The strings will be Short date and time converted to string form
        List<string> ViewbagDates(IPatientAppointmentReferralRepository PARrepository, IAppointmentRepository AppointmentRepository, string MedicalPersonnel, DateTime date , ITimesRepository timesRepository);
    }
}
