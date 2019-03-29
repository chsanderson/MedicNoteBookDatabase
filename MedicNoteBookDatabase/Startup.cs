using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MedicNoteBookDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System.Security.Cryptography;
//using Microsoft.AspNetCore.DataProtection;

namespace MedicNoteBookDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(Configuration["Data:MedicNoteBook:ConnectionStrings"]));
            //this confirms the connections between the interface classes  and the Entity Framework classes
            services.AddTransient<IPatientAppointmentReferralRepository, EFPatientAppointmentReferralRepository>();
            services.AddTransient<IAppointmentRepository, EFAppointmentRepository>();
            services.AddTransient<IAddressRepository, EFAddressRepository>();
            services.AddTransient<IContactDetailsRepository, EFContactDetailsRepository>();
            services.AddTransient<IPracticeInfoRepository, EFPracticeInfoRepository>();
            services.AddTransient<IRoleRepository, EFRoleRepository>();
            services.AddTransient<ITimesRepository, EFTimesRepository>();

            services.AddDbContext<ApplicationIdentityDBContext>(options =>
            options.UseSqlServer(Configuration["Data:Identity:ConnectionStrings"]));
            services.AddTransient<IAccountRepository, EFAccountRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //added new session options
            services.AddDistributedMemoryCache();
            services.AddScoped<IDataProtectionService, DataProtectionService>();
            services.AddDataProtection();
            //this creates a cookie to allow sessions to be stored
            services.AddSession(options =>
            {
            //    options.Cookie.Name = "ids";
              options.IdleTimeout = TimeSpan.FromMinutes(40);
            //    options.Cookie.HttpOnly = true;
            });
            services.AddMemoryCache();
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"c:\MedicNoteBook\keys")
                );
            //this is one attempt at allowing strings variables to be encrypted and decrypted
            //@"\\server\share\directory\"));
            //services.AddDataProtection()
            //    .UseCryptographicAlgorithms(
            //    new ManagedAuthenticatedEncryptorConfiguration()
            //    {
            //        EncryptionAlgorithmType = typeof(Aes),
            //        EncryptionAlgorithmKeySize = 256,
            //        ValidationAlgorithmType = typeof(HMACSHA256)
            //    });

            /*EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256*/

            /*services.ConfigureDataProtection(dp =>
            {
                dp.PersistKeysToFileSystem(new DirectoryInfo(@"c:\keys"));
                dp.SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
            //SeedData.EnsurePopulated(app);
        }
    }
}
