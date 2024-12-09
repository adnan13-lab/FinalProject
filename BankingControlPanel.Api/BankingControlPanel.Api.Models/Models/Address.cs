using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankingControlPanel.Api.Models.Models
{
    public class Address
    {
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

        [ForeignKey("ClientId")]
        public int ClientId { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }
    }
}
