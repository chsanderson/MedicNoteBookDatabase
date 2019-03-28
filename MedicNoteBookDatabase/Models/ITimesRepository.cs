using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface ITimesRepository
    {
        IQueryable<Times> Times { get; }

        List<string> ViewbagDates(IPatientAppointmentReferralRepository PARrepository, IAppointmentRepository AppointmentRepository, string MedicalPersonnel, DateTime date , ITimesRepository timesRepository);
    }
}
