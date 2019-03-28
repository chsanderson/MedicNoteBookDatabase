using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFTimesRepository : ITimesRepository
    {
        private ApplicationDBContext Context;

        public EFTimesRepository(ApplicationDBContext applicationDBContext)
        {
            Context = applicationDBContext;
        }

        public IQueryable<Times> Times => Context.Times;

        public List<string> ViewbagDates(IPatientAppointmentReferralRepository PARrepository, IAppointmentRepository AppointmentRepository, string MedicalPersonnel, DateTime date, ITimesRepository timesRepository)
        {
            List<string> times = new List<string>();
            List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();
            List<Appointment> appointment = new List<Appointment>();
            List<int> dateValues = new List<int>();
            PAR = PARrepository.PAR.Where(a => a.RequestedDate >= DateTime.Now.Date).ToList();
            appointment = AppointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now.Date).ToList();
            List<string> dateTimes = new List<string>();
            if(date > DateTime.Now)
            {
                dateTimes.Add(date.ToString());
            }
            DateTime days = DateTime.Now;
            for (int i = 0; i <= 31; i++)
            {
                DateTime day = DateTime.Now.AddDays(i);
                DateTime dates;
                if (MedicalPersonnel.Length > 0)
                {
                    Appointment appointmentDay = appointment.FirstOrDefault(d => d.AppointmentDate == day);
                    PatientAppointmentReferral patientAppointmentDay = PAR.FirstOrDefault(p => p.RequestedDate == day);
                    for (int t = 1; t <= timesRepository.Times.Count(); t++)
                    {
                        Times time = timesRepository.Times.FirstOrDefault(v => v.ID == t);
                        try
                        {
                            dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.Substring(0, 2)), int.Parse(time.Time.Substring(3)), 0);
                            if ((dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday) && ((appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentDate.ToLongTimeString().Contains(time.Time)) == false || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedDate.ToLongTimeString().Contains(time.Time) == false)))
                            {
                                if (dates > days)
                                {
                                    List<PatientAppointmentReferral> patientAppointmentReferrals = new List<PatientAppointmentReferral>();
                                    patientAppointmentReferrals = PARrepository.PAR.Where(p => p.MedicalPersonnel == MedicalPersonnel).Where(p => p.RequestedDate == dates).ToList();//.RequestedDate = dates;//.Where(p => p.RequestedDate == dates);

                                    List<Appointment> appointments = new List<Appointment>();
                                    appointments = AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional == MedicalPersonnel).Where(a => a.AppointmentDate == dates).ToList();

                                    if (patientAppointmentReferrals.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                    {
                                        if (appointments.Count() == 0)
                                        {
                                            dateTimes.Add(dates.ToString());/*Date.*/
                                        }
                                    }
                                    else if (appointments.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                    {
                                        if (patientAppointmentReferrals.Count() == 0)
                                        {
                                            dateTimes.Add(dates.ToString());/*Date.*/
                                        }
                                    }
                                    //if (patientAppointmentReferrals.Count() == 0 )/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                    //{
                                    //    if(appointments.Count() == 0)
                                    //    { 
                                    //        dateTimes.Add(dates.ToString());/*Date.*/
                                    //    }
                                    //}
                                }
                            }
                        }
                        catch
                        {
                            try
                            {
                                dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(2)), 0);
                                if ((dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday) && (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
                                {
                                    if (dates > days)
                                    {
                                        //if ((PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))

                                        List<PatientAppointmentReferral> patientAppointmentReferrals = new List<PatientAppointmentReferral>();
                                        patientAppointmentReferrals = PARrepository.PAR.Where(p => p.MedicalPersonnel == MedicalPersonnel).Where(p => p.RequestedDate == dates).ToList();//.RequestedDate = dates;//.Where(p => p.RequestedDate == dates);

                                        List<Appointment> appointments = new List<Appointment>();
                                        appointments = AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional == MedicalPersonnel).Where(a => a.AppointmentDate == dates).ToList();

                                        if (patientAppointmentReferrals.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        {
                                            if (appointments.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());/*Date.*/
                                            }
                                        }
                                        else if (appointments.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        {
                                            if (patientAppointmentReferrals.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());/*Date.*/
                                            }
                                        }
                                        //if (patientAppointmentReferrals.Count() > 0 || appointments.Count() > 0)/*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        //{
                                        //    dateTimes.Add(dates.ToString());
                                        //}
                                    }
                                }
                            }
                            catch
                            {
                                dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(3)), 0);
                                if (dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
                                {
                                    if (dates > days)
                                    {
                                        //if ((PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))

                                        List<PatientAppointmentReferral> patientAppointmentReferrals = new List<PatientAppointmentReferral>();
                                        patientAppointmentReferrals = PARrepository.PAR.Where(p => p.MedicalPersonnel == MedicalPersonnel).Where(p => p.RequestedDate == dates).ToList();//.RequestedDate = dates;//.Where(p => p.RequestedDate == dates);

                                        List<Appointment> appointments = new List<Appointment>();
                                        appointments = AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional == MedicalPersonnel).Where(a => a.AppointmentDate == dates).ToList();

                                        if (patientAppointmentReferrals.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        {
                                            if (appointments.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());/*Date.*/
                                            }
                                        }
                                        else if (appointments.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        {
                                            if (patientAppointmentReferrals.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());/*Date.*/
                                            }
                                        }
                                        //if (patientAppointmentReferrals.Count() == 0)/*||*/ /*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        //{
                                        //    if (appointments.Count() == 0)
                                        //    {
                                        //        dateTimes.Add(dates.ToString());/*Date.*/
                                        //    }
                                        //}
                                        //if (patientAppointmentReferrals.Count() > 0 || appointments.Count() > 0)/*(PARrepository.PAR.Where(p => p.MedicalPersonnel != MedicalPersonnel).Where(p => p.RequestedDate == dates) == null) || (AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional != MedicalPersonnel).Where(a => a.AppointmentDate == dates) == null))*/
                                        //{
                                        //    dateTimes.Add(dates.ToString());
                                        //}
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dateTimes;
        }
    }
}
