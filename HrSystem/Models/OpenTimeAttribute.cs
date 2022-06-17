using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class OpenTimeAttribute:ValidationAttribute
    {
        public string Time { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime MinTime = DateTime.Parse(Time);

            string Message = string.Empty;
            if (Convert.ToDateTime(value) < MinTime)
            {
                Message = "cannot be Before 8 AM";
                return new ValidationResult(Message);
            }
            return ValidationResult.Success;
        }
    }
}
