using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankingControlPanel.Api.Models.Models
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
        [RegularExpression("^((\\+92)?(0092)?(92)?(0)?)(3)([0-9]{9})$", ErrorMessage = "Mobile number must follow the correct Pakistani format.")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public string? Sex { get; set; }

        public virtual Address? Address { get; set; }

        public virtual ICollection<Account>? Account { get; set; }

        [JsonIgnore]
        public virtual Search? Search { get; set; }
    }
}
