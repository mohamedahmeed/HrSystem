using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Department
    {
        public int ID { get; set; }
        [Required]
        [UniqueD]
        public string Name { get; set; }
        public virtual List<Employee> Employees { get; set; }

    }
}
