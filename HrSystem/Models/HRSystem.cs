using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Models
{
    public class HRSystem : IdentityDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<GeneralSetting> General_Settings { get; set; }
        public DbSet<OfficialHolidays> OfficialHolidays { get; set; }
        public DbSet<Attendance_Leaving> Attendance_Leavings { get; set; }



        public HRSystem(DbContextOptions options) : base(options)
        {

        }
        public HRSystem()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HRSystem;Integrated Security=True");
        }
    }
}
