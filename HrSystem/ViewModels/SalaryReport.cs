using System.ComponentModel.DataAnnotations;

namespace HrSystem.ViewModel
{


    public class SalaryReport
    {
        [Required]
        public string Search { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public int AttendanceDayNumbers { get; set; }
        public int AbsenceDayNumbers { get; set; }

        public float ExtraHours { get; set; }
        public float DeductionHours { get; set; }

        public float TotalBonus { get; set; }
        public float TotalDeduction { get; set; }

        public float NetSalary { get; set; }

    }
}
