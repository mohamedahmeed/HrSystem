using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GraduationProject.Models
{
    public class UniqueD:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HRSystem Context = new HRSystem();
            Department Department = Context.Departments.FirstOrDefault(c => c.Name == value.ToString());
            Department Dept = validationContext.ObjectInstance as Department;

            if (Department == null || Department.ID == Dept.ID)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Name Already Exist");
        }
    }
}
