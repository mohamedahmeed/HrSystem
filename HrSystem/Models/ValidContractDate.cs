using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class ValidContractDate: ValidationAttribute
    {
        public string Date { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime MinDate = DateTime.Parse(Date);
            string Message = string.Empty;
            if (Convert.ToDateTime(value) < MinDate)
            {
                Message = "Contract date cannot be before 1/1/2008";
                return new ValidationResult(Message);
            }
            return ValidationResult.Success;
        }
    }
}
