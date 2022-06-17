using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Attendance_Leaving
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Attendance Time")]

        public string AttendanceTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name ="Leaving Time")]

        public string LeavingTime { get; set; }
        [Required]
        [Display(Name = "Employee")]

        [ForeignKey("Emp")]
        public int Emp_ID { get; set; }
        [Display(Name = "Employee Name")]

        public virtual Employee Emp { get; set; }
    }
}
