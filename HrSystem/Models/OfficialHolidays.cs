using HrSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class OfficialHolidays
    {
        public int ID { get; set; }
        [UniqueHolidays]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [UniqueDate]
        public System.DateTime Date { get; set; }

    }
}
