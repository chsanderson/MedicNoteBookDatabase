//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{

    public interface IPatientAppointmentReferralRepository
    {
        IQueryable<PatientAppointmentReferral> PAR { get; }
        
        //early version of updatePAR
        //this creates an instance of a method that allows the editing of guest appointments
        //void SavePAR(PatientAppointmentReferral par);

        //this creates an instance of a method that allows the editing of guest appointments
        void UpdatePAR(/*int id, */PatientAppointmentReferral par);

        //this creates an instance of a method that allows the creation of guest appointments
        void CreatePAR(PatientAppointmentReferral par);

        //this creates an instance of a method that allows the deleting of guest appointments
        void DeletePAR(/*int id,*/ PatientAppointmentReferral par);

        //this creates an instance of a method that returns a list full of PatientAppointment Referrals
        List<PatientAppointmentReferral> ViewBagPAR(string MedicalProfessional);

    }
}
