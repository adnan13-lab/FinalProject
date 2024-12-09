using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Models
{
    public class Searchs
    {
        [Key]
        public int SeachId { get; set; }

        [Required(ErrorMessage = "Search Name is required.")]
        [MaxLength(59, ErrorMessage = "Search Name cannot exceed 59 characters.")]
        public string? Name { get; set; }
    }
}
