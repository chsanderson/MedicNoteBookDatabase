using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IPatientAppointmentReferralRepository
    {
        IQueryable<PatientAppointmentReferral> PAR { get; }

        void SavePAR(PatientAppointmentReferral par);

        void UpdatePAR(/*int id, */PatientAppointmentReferral par);

        //bool CreatePAR(PatientAppointmentReferral par);

        void CreatePAR(PatientAppointmentReferral par);

        void DeletePAR(/*int id,*/ PatientAppointmentReferral par);

        List<PatientAppointmentReferral> ViewBagPAR(string MedicalProfessional);

    }
}
