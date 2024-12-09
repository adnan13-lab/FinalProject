using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BankingControlPanel.Validations
{
    public class PakistaniPhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Phone number is required.");
            }

            var phoneNumber = value.ToString();
            var pattern = @"^((\+92)?(0092)?(92)?(0)?)(3)([0-9]{9})$";

            if (Regex.IsMatch(phoneNumber!, pattern))
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "Invalid phone number format. Please use +92XXXXXXXXXX or 03XXXXXXXXX.");
            }
        }
    }
}