using System;
using System.ComponentModel.DataAnnotations;

namespace EventCardCopilotApp.Models.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
           if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.Now)
                {
                    var errorMessage = string.IsNullOrEmpty(ErrorMessage)
                        ? "La data deve essere nel futuro."
                        : ErrorMessage;

                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}