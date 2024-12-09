using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankingControlPanel.Api.Models.Models
{
    public class Search
    {
        [Key]
        public int SeachId { get; set; }

        [Required(ErrorMessage = "Search Name is required.")]
        [MaxLength(59, ErrorMessage = "Search Name cannot exceed 59 characters.")]
        public string? Name { get; set; }

        [ForeignKey("ClientId")]
        public int ClientId { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }
    }
}
