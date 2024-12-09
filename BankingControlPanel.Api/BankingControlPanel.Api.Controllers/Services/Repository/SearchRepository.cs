using Azure;
using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Api.Controllers.Services.Repository
{
    public class SearchRepository : ISearch
    {
        public readonly BankingControlPanelDBcontext _bankingControlPanelDBcontext;

        // Constructor to initialize the database context
        public SearchRepository(BankingControlPanelDBcontext bankingControlPanelDBcontext)
        {
            this._bankingControlPanelDBcontext = bankingControlPanelDBcontext;
        }

        // Method to retrieve all search records
        public async Task<ActionResult<List<Search>>> GetSearchs()
        {
            var response = await _bankingControlPanelDBcontext.Searches.ToListAsync();
            return response;
        }

        // Method to retrieve a specific search record by its ID
        public async Task<ActionResult<Search>> GetSearchById(int id)
        {
            var response = await _bankingControlPanelDBcontext.Searches.FindAsync(id);
            return response!;
        }

        // Method to add a new search entry, based on a client's first name
        public async Task<ActionResult<Search>> AddSearch(Search search)
        {
            // Look for a client with the given first name
            var client = await _bankingControlPanelDBcontext.Clients
                .Where(e => e.FirstName == search.Name)
                .FirstOrDefaultAsync();

            // If client is not found, return null
            if (client == null)
            {
                return null!;
            }

            // Check if a search entry for this client already exists
            var existingSearch = await _bankingControlPanelDBcontext.Searches
                .FirstOrDefaultAsync(e => e.ClientId == client.ClientId);

            if (existingSearch != null)
            {
                // If it exists, return the existing search
                return existingSearch;
            }

            // Otherwise, create a new search entry
            var searchData = new Search
            {
                Name = search.Name,
                ClientId = client.ClientId
            };

            // Add the new search to the database
            await _bankingControlPanelDBcontext.Searches.AddAsync(searchData);
            await _bankingControlPanelDBcontext.SaveChangesAsync();

            return searchData;
        }

        // Method to delete a search entry by its ID
        public async Task<ActionResult<Search>> DeleteSearch(int id)
        {
            var response = await _bankingControlPanelDBcontext.Searches.FindAsync(id);

            // Remove the search entry from the database
            _bankingControlPanelDBcontext.Searches.Remove(response!);
            await _bankingControlPanelDBcontext.SaveChangesAsync();

            return response!;
        }
    }
}
