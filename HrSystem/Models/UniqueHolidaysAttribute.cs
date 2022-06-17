using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GraduationProject.Models
{
    public class UniqueHolidaysAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HRSystem Context = new HRSystem();
            OfficialHolidays Holiday = Context.OfficialHolidays.FirstOrDefault(c => c.Name == value.ToString());
            OfficialHolidays Holi = validationContext.ObjectInstance as OfficialHolidays;

            if (Holiday == null || Holiday.ID == Holi.ID)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Name Already Exist");
        }
    }
}
