using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

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
    }
}
