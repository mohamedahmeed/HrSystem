using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Employee
    {
        //validation 
        public int ID { get; set; }
        [Required]
        [UniqueEmployee]
        public string Name { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]
        [RegularExpression("^01[0125][0-9]{8}$",ErrorMessage ="Phone has to be 11 numbers")]
        public string Phone { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public string Nationality { get; set; }
        [Required]
        [MaxLength(14,ErrorMessage = "Enter Valid National ID")]
        [MinLength(14, ErrorMessage = "Enter Valid National ID")]
        [RegularExpression(@"(2|3)[0-9][1-9][0-1][1-9][0-3][1-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessage = "Enter Valid National ID")]
        [UniqueID]
        public string NationalityID { get; set; }
        [Required]
        [vaildAge(Date = "1/1/2002")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDay { get; set; }
        [Required]

        [Display(Name = "Hiring Date")]
        [ValidContractDate(Date = "1/1/2008")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ContractDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Attendance")]
        public string AttendanceTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Leaving")]
        public string LeavingTime { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage ="Salary should be a number")]
        public float Salary { get; set; }
       

        [ForeignKey("Dept")]
        [Required]
        [Display(Name = "Department")]
        public int Dept_ID { get; set; }
        [Display(Name = "Department")]
        public virtual Department Dept { get; set; }
      
        public virtual List<Attendance_Leaving> Attendance_Leavings { get; set; }
    }
}
