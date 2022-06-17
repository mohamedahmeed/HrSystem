using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
    public class SearchAttendanceViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Search { get; set; }
        [DataType(DataType.Date)]
        [Required]

        public System.DateTime From { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public System.DateTime To { get; set; }
        public string Department { get; set; }
        public string EmployeeName { get; set; }
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [DataType(DataType.Time)]

        public string AttendanceTime { get; set; }
        [DataType(DataType.Time)]

        public string LeavingTime { get; set; }
    }
}
