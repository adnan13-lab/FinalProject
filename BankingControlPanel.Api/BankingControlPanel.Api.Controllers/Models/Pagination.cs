using BankingControlPanel.Api.Models.Models;

namespace BankingControlPanel.Api.Controllers.Models
{
    public class Pagination
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<Client>? Clients { get; set; }
    }
}
