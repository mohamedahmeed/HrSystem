
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GraduationProject.Models
{
    public class UniqueEmployeeAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HRSystem dp=new HRSystem();
            Employee employee=dp.Employees.FirstOrDefault(s=>s.Name==value.ToString());
            Employee emp=validationContext.ObjectInstance as Employee;
            if (employee == null||employee.ID==emp.ID)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Name Already Exist");
        }
    }
}
