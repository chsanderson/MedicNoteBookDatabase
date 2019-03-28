using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class ApplicationIdentityDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationIdentityDBContext(DbContextOptions<ApplicationIdentityDBContext> options) : base(options) { }

        //public DbSet<Account> Account { get; set; }
    }
    //public class ApplicationIdentityDBContext : DbContext
    //{
    //    public ApplicationIdentityDBContext(DbContextOptions<ApplicationIdentityDBContext> options) : base( options ) { }

    //    //public DbSet<Account> Account { get; set; }
    //}
}
