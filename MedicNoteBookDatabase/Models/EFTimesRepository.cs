//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    //this contains the code for the instances created in ITimesRepository
    public class EFTimesRepository : ITimesRepository
    {
        //this creates an variable of the datatype of ApplicationDBContext that is connected to the database
        private ApplicationDBContext Context;

        public EFTimesRepository(ApplicationDBContext applicationDBContext)
        {
            //this assigns a new value of the datatype ApplicationDBContext to the variable above
            Context = applicationDBContext;
        }

        //this sets the instance of the Iquerable variable to the database table Times
        public IQueryable<Times> Times => Context.Times;

        //this returns a list of strings when this method is called
        public List<string> ViewbagDates(IPatientAppointmentReferralRepository PARrepository, IAppointmentRepository AppointmentRepository, string MedicalPersonnel, DateTime date, ITimesRepository timesRepository)
        {
            //this creates an instance of a variable called times with has a datatype of List and contains string variables
            List<string> times = new List<string>();

            //this creates an instance of a variable PAR which has a datatype of List which contains records of the PatientApplicationReferral model 
            List<PatientAppointmentReferral> PAR = new List<PatientAppointmentReferral>();

            //this creates an instance of a variable appointment which has a datatype of List which contains records of the Appointment model 
            List<Appointment> appointment = new List<Appointment>();

            //this creates an instance of a variable called dateValues with has a datatype of List and contains integer variables
            List<int> dateValues = new List<int>();

            //this assigns the PAR with a list of the PatientAppointmentReferrals in the database where the date and time is in the future compared to the current data and time
            PAR = PARrepository.PAR.Where(a => a.RequestedDate >= DateTime.Now.Date).ToList();
  
            //this assigns the PAR with a list of the PatientAppointmentReferrals in the database where the date and time is in the future compared to the current data and time
            appointment = AppointmentRepository.Appointment.Where(a => a.AppointmentDate >= DateTime.Now.Date).ToList();

            //this creates an instance of a variable called dateTimes with has a datatype of List and contains string variables
            List<string> dateTimes = new List<string>();

            //this checks if the date supplied has a greater value than the current date
            if(date > DateTime.Now)
            {
                //this converts the date to a string and adds it to the dateTimes list
                dateTimes.Add(date.ToString());
            }

            //this creates a new instance of the current date and time and assigns it to the a DateTime variable called days
            DateTime days = DateTime.Now;

            //this for loop allows the same action to be performed for the next 31 days
            for (int i = 0; i <= 31; i++)
            {
                // this creates a DateTime variable  and assigns it the value of the current day with days being added to it relevant to the value of i
                DateTime day = DateTime.Now.AddDays(i);

                //this creates a DateTime variable called dates
                DateTime dates;

                //this checks if the if the medical personnel supplied has a string length greater to 0
                if (MedicalPersonnel.Length > 0)
                {
                   //this creates variables to make sure that their is no records in the database
                    Appointment appointmentDay = appointment.FirstOrDefault(d => d.AppointmentDate == day);
                    PatientAppointmentReferral patientAppointmentDay = PAR.FirstOrDefault(p => p.RequestedDate == day);
                    //this checks all of the times in the database so that the times can be supplied to the user
                    for (int t = 1; t <= timesRepository.Times.Count(); t++)
                    {
                        //this assigns a time model where the id in the database matches the value of t
                        Times time = timesRepository.Times.FirstOrDefault(v => v.ID == t);
                        //this tries many times to see if the value can be added to the list
                        try
                        {
                            //this creates a custom date using the values supplied and parsing them to int
                            dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.Substring(0, 2)), int.Parse(time.Time.Substring(3)), 0);

                            //this makes sure that no appointments can be made for saturday or sunday and that the appointment day and time is not currently allocated to the medical professional currently being used
                            if ((dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday) && ((appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentDate.ToLongTimeString().Contains(time.Time)) == false || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedDate.ToLongTimeString().Contains(time.Time) == false)))
                            {
                                //this checks that the dates variable is greater than the current date
                                if (dates > days)
                                {
                                    List<PatientAppointmentReferral> patientAppointmentReferrals = new List<PatientAppointmentReferral>();
                                    patientAppointmentReferrals = PARrepository.PAR.Where(p => p.MedicalPersonnel == MedicalPersonnel).Where(p => p.RequestedDate == dates).ToList();//.RequestedDate = dates;//.Where(p => p.RequestedDate == dates);

                                    List<Appointment> appointments = new List<Appointment>();
                                    appointments = AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional == MedicalPersonnel).Where(a => a.AppointmentDate == dates).ToList();

                                    //this checks if the lists contained in the patientAppointmentReferrals variable or the appointments variable doesn't contain
                                    //the datetime variable then convert it to string and add to the dateTimes list
                                    if (patientAppointmentReferrals.Count() == 0)
                                    {
                                        if (appointments.Count() == 0)
                                        {
                                            dateTimes.Add(dates.ToString());
                                        }
                                    }
                                    else if (appointments.Count() == 0)
                                    {
                                        if (patientAppointmentReferrals.Count() == 0)
                                        {
                                            dateTimes.Add(dates.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            try
                            {
                                //this creates a custom date using the values supplied and parsing them to int
                                dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(2)), 0);

                                //this makes sure that no appointments can be made for saturday or sunday and that the appointment day and time is not currently allocated to the medical professional currently being used
                                if ((dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday) && (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
                                {
                                    //this checks that the dates variable is greater than the current date
                                    if (dates > days)
                                    {

                                        List<PatientAppointmentReferral> patientAppointmentReferrals = new List<PatientAppointmentReferral>();
                                        patientAppointmentReferrals = PARrepository.PAR.Where(p => p.MedicalPersonnel == MedicalPersonnel).Where(p => p.RequestedDate == dates).ToList();//.RequestedDate = dates;//.Where(p => p.RequestedDate == dates);

                                        List<Appointment> appointments = new List<Appointment>();
                                        appointments = AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional == MedicalPersonnel).Where(a => a.AppointmentDate == dates).ToList();

                                        //this checks if the lists contained in the patientAppointmentReferrals variable or the appointments variable doesn't contain
                                        //the datetime variable then convert it to string and add to the dateTimes list

                                        if (patientAppointmentReferrals.Count() == 0)
                                        {
                                            if (appointments.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());
                                            }
                                        }
                                        else if (appointments.Count() == 0)
                                        {
                                            if (patientAppointmentReferrals.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                //this creates a custom date using the values supplied and parsing them to int
                                dates = new DateTime(int.Parse(day.Date.Year.ToString()), int.Parse(day.Date.Month.ToString()), int.Parse(day.Date.Day.ToString()), int.Parse(time.Time.ToString().Substring(0, 1)), int.Parse(time.Time.ToString().Substring(3)), 0);

                                //this makes sure that no appointments can be made for saturday or sunday and that the appointment day and time is not currently allocated to the medical professional currently being used
                                if (dates.DayOfWeek != DayOfWeek.Saturday || dates.DayOfWeek != DayOfWeek.Sunday || (appointmentDay == null || patientAppointmentDay == null) || (appointmentDay.AppointmentMedicalProfessional != MedicalPersonnel && appointmentDay.AppointmentTime != time.Time) || (patientAppointmentDay.MedicalPersonnel != MedicalPersonnel && patientAppointmentDay.RequestedTime != time.Time))
                                {

                                    //this checks that the dates variable is greater than the current date
                                    if (dates > days)
                                    {
                                        List<PatientAppointmentReferral> patientAppointmentReferrals = new List<PatientAppointmentReferral>();
                                        patientAppointmentReferrals = PARrepository.PAR.Where(p => p.MedicalPersonnel == MedicalPersonnel).Where(p => p.RequestedDate == dates).ToList();

                                        List<Appointment> appointments = new List<Appointment>();
                                        appointments = AppointmentRepository.Appointment.Where(a => a.AppointmentMedicalProfessional == MedicalPersonnel).Where(a => a.AppointmentDate == dates).ToList();

                                        //this checks if the lists contained in the patientAppointmentReferrals variable or the appointments variable doesn't contain
                                        //the datetime variable then convert it to string and add to the dateTimes list
                                        if (patientAppointmentReferrals.Count() == 0)
                                        {
                                            if (appointments.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());
                                            }
                                        }
                                        else if (appointments.Count() == 0)
                                        {
                                            if (patientAppointmentReferrals.Count() == 0)
                                            {
                                                dateTimes.Add(dates.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //this returns the values stored in the dateTimes variable
            return dateTimes;
        }
    }
}
