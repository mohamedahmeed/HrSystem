
using GraduationProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HrSystem.Models
{
    public class UniqueDateAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HRSystem Context = new HRSystem();
            OfficialHolidays Holiday = Context.OfficialHolidays.FirstOrDefault(c => c.Date.Equals(value));
            OfficialHolidays Holi = validationContext.ObjectInstance as OfficialHolidays;

            if (Holiday == null || Holiday.ID == Holi.ID)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Date Is Already Exist");
        }
    }
}
