using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BankingControlPanel.Models
{
    public class Client
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

        [Required(ErrorMessage = "Profile Path is required.")]
        public string? ProfilePath { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression("^\\+([1-9]{1}[0-9]{1,3})\\s?[0-9]{4,14}$", ErrorMessage = "Mobile number must follow the correct international format.")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public string? Sex { get; set; }

        public Address? address { get; set; }
        public List<Account>? account { get; set; }


        
    }
}
