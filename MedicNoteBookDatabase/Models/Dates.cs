using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicNoteBookDatabase.Models;
using Microsoft.AspNetCore.Http;

namespace MedicNoteBookDatabase.Models
{
    public class Dates
    {
        //public List<string> ViewbagDates(IPatientAppointmentReferralRepository PARrepository, IAppointmentRepository AppointmentRepository, string MedicalPersonnel, ITimesRepository timesRepository)
        //{
        //    List<string> times = new List<string>();
        //    List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();
        //    List<Appointment> appointment = new List<Appointment>();
        //    List<int> dateValues = new List<int>();
        //    PAR = PARrepository.PAR.Where(a => a.RequestedDate >= DateTime.Now.Date).ToList();
        //    appointment = AppointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now.Date).ToList();
        //    List<string> dateTimes = new List<string>();
        //    DateTime days = DateTime.Now;
        //    for (int i = 0; i <= 31; i++)
        //    {
        //        DateTime day = DateTime.Now.AddDays(i);
        //        DateTime dates;
        //        if (MedicalPersonnel.Length > 0)
        //        {
        //            Appointment appointmentDay = appointment.FirstOrDefault(d => d.AppointmentDate == day);
        //            PatientAppointmentReferral patientAppointmentDay = PAR.FirstOrDefault(p => p.RequestedDate == day);
        //            for (int t = 1; t <= timesRepository.Times.Count(); t++)
        //            {
        //                Times time = timesRepository.Times.FirstOrDefault(v => v.ID == t);
        //                try
        //                {
        //                    dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time/*.ToString()*/.Substring(0, 2)), int.Parse(time.Time/*.ToString()*/.Substring(3)), 0);
        //                    if (dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel/*account.MedicalPersonnel*/ && patientAppointmentDay.RequestedTime != time.Time))
        //                    {
        //                        if (dates > days)
        //                        {
        //                            dateTimes.Add(dates.ToString());/*Date.*/
        //                        }
        //                    }
        //                }
        //                catch
        //                {
        //                    try
        //                    {
        //                        dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(2)), 0);
        //                        if ((dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday) && (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
        //                        {
        //                            if (dates > days)
        //                            {
        //                                dateTimes.Add(dates.ToString());
        //                            }
        //                        }
        //                    }
        //                    catch
        //                    {
        //                        dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(3)), 0);
        //                        if (dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
        //                        {
        //                            if (dates > days)
        //                            {
        //                                dateTimes.Add(dates.ToString());
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return dateTimes;
        //}
    }
}
