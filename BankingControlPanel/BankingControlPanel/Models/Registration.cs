using BankingControlPanel.Validations;
using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Models
{
    public class Registration
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(59, ErrorMessage = "First Name cannot exceed 59 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(59, ErrorMessage = "Last Name cannot exceed 59 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Personal ID is required.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal ID must be exactly 11 characters long.")]
        public string? PersonalId { get; set; }
        
        public string? ProfilePath { get; set; }

        public IFormFile? Image { get; set; }


        [Required(ErrorMessage = "Mobile number is required.")]
        [PakistaniPhoneNumberValidation(ErrorMessage = "Mobile number must be in a valid Pakistani format. Use +92XXXXXXXXXX or 03XXXXXXXXX.")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [MaxLength(200, ErrorMessage = "Street cannot exceed 200 characters.")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [MaxLength(20, ErrorMessage = "Zip Code cannot exceed 20 characters.")]
        public string? ZipCode { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
            ErrorMessage = "Email must be in a valid format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string? Role { get; set; }
    }
}
