using GraduationProject.Models;
using GraduationProject.Services;
using HrSystem.services;
using HrSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem
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
            services.AddControllersWithViews();
            services.AddDbContext<HRSystem>(option => option.UseSqlServer(Configuration.GetConnectionString("Local")));
            services.AddControllersWithViews();
            services.AddScoped<IRepository<Department>,DepartmentServices>();
            services.AddScoped<IRepository<Employee>,EmployeeService>();
            services.AddScoped<IRepository<GeneralSetting>,GeneralService>();
            services.AddScoped<IRepository<OfficialHolidays>, HolidayServices>();
            services.AddScoped<IRepository<Attendance_Leaving>, AttendanceServices>();
            services.AddIdentity<IdentityUser, IdentityRole>(optios =>
            {
                optios.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<HRSystem>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
