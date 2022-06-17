
using System;
using System.ComponentModel.DataAnnotations;


namespace GraduationProject.Models
{
    public class vaildAge : ValidationAttribute
    {
        public string Date { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime MinDate = DateTime.Parse(Date);
            string Message = string.Empty;
            if (Convert.ToDateTime(value) > MinDate)
            {
                Message = "Brith date cannot be after 1/1/2002";
                return new ValidationResult(Message);
            }
            return ValidationResult.Success;

        }

    }
}


   

    

