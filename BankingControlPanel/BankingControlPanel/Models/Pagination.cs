using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Models
{
    public class Pagination
    {
        [Required]
        public int TotalRecords { get; set; }
        [Required]
        public int TotalPages { get; set; }
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int PageSize { get; set; }
        [Required]
        public List<Client>? Clients { get; set; }

    }
}
