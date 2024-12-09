using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Services.Core
{
    // Interface for Search operations in the system
    public interface ISearch
    {
        // Method to retrieve a list of all search records
        public Task<ActionResult<List<Search>>> GetSearchs();

        // Method to retrieve a specific search by its ID
        public Task<ActionResult<Search>> GetSearchById(int id);

        // Method to add a new search record
        public Task<ActionResult<Search>> AddSearch(Search search);

        // Method to delete a search record by its ID
        public Task<ActionResult<Search>> DeleteSearch(int id);
    }
}
