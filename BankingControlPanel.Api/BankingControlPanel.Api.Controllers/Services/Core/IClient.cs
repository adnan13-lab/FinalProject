using BankingControlPanel.Api.Controllers.Models;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Services.Core
{
    // Interface defining methods for CRUD operations and additional features on clients
    public interface IClient
    {
        // Method to retrieve a list of all clients
        public Task<ActionResult<List<Client>>> GetClients();

        // Method to retrieve a single client by their ID
        public Task<ActionResult<Client>> GetClientById(int id);

        // Method to add a new client
        public Task<ActionResult<Client>> AddClient(Client client);

        // Method to update an existing client's information
        public Task<ActionResult<Client>> UpdateClient(int id, Client client);

        // Method to delete a client by their ID
        public Task<ActionResult<Client>> DeleteClient(int id);

        // Method for paginating the list of clients, allowing a specified page number and size
        public Task<ActionResult<Pagination>> Pagination(int pageNum, int PageSize, string sort);

        // Method to filter clients based on their name
        public Task<ActionResult<List<Client>>> FilterClient(string name);
    }
}
