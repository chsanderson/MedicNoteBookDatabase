//Christopher Sanderson
//MedicNoteBook
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class ApplicationDBContext : DbContext 
    {
        //this creates a databaseSet for each of the tables referenced in the database and connects to the MedicNoteBook Database
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Account> Account { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<PatientAppointmentReferral> PatientAppointmentReferral { get; set; }
        public DbSet<PracticeInfo> PracticeInfo { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Times> Times { get; set; }

    }
}
