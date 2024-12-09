using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Controllers
{
    // ApiController attribute ensures automatic model validation and binding of HTTP requests
    [ApiController]
    // Route attribute to map this controller to the "api/client" endpoint
    [Route("api/[controller]")]
    // Authorization attribute to restrict access to only users with "Admin" role
    
    public class ClientController : ControllerBase
    {
        // Declare the IClient service which will handle the client-related logic
        public readonly IClient _client;

        // Constructor to inject the IClient service dependency into the controller
        public ClientController(IClient client)
        {
            this._client = client;
        }

        // GET api/client - Fetches the list of clients
        [HttpGet]
        public async Task<ActionResult> ClientList()
        {
            try
            {
                // Fetch the client data from the service
                var response = await _client.GetClients();

                // If there are no clients, return a 404 (Not Found) response
                if (response!.Value!.Count == 0)
                {
                    return NotFound("Client List Is Empty");
                }

                // Return the client data as a successful response (200 OK)
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // GET api/client/{id} - Fetches a client by its ID
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult> ClientById(int id)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Fetch client data by ID
                var response = await _client.GetClientById(id);

                // If the client is not found, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Client Not Found");
                }

                // Return the client data as a successful response (200 OK)
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // POST api/client - Adds a new client
        [HttpPost]
        public async Task<ActionResult> AddClient(Client client)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Add the new client using the service
                var response = await _client.AddClient(client);

                if (response == null)
                {
                    return BadRequest("Email Already Exists");
                }
                // If the client was not added successfully, return a BadRequest response
                if (response.Value == null)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Return the added client data as a successful response (200 OK)
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // PUT api/client/{id} - Updates an existing client by its ID
        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, Client client)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Update the client data using the service
                var response = await _client.UpdateClient(id, client);

                // If the client is not found, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Client Not Found");
                }

                // Return the updated client data as a successful response (200 OK)
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // GET api/client/Pagination - Fetches a paginated list of clients
        [Authorize(Roles = "Admin")]
        [HttpGet("Pagination")]
        public async Task<ActionResult<List<Client>>> Pagination(int pageNum, int PageSize,string sort)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Fetch the paginated list of clients from the service
                var response = await _client.Pagination(pageNum,PageSize,sort);

                // If no clients were found, return a 404 (Not Found) response
                if (response == null)
                {
                    return NotFound("Client Not Found");
                }
                else
                {
                    // Return the paginated client data as a successful response (200 OK)
                    return Ok(response.Value);
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // POST api/client/FilterClient - Filters the clients by name
        [Authorize(Roles = "Admin")]
        [HttpGet("FilterClient")]
        public async Task<ActionResult<List<Client>>> FilterClient(string name)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Filter clients by the provided name
                var response = await _client.FilterClient(name);

                // If no clients were found, return a 404 (Not Found) response
                if (response!.Value!.Count == 0)
                {
                    return NotFound("Client Not Found");
                }
                else
                {
                    // Return the filtered client data as a successful response (200 OK)
                    return Ok(response.Value);
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // DELETE api/client/{id} - Deletes a client by its ID
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Delete the client using the service
                var response = await _client.DeleteClient(id);

                // If the client is not found, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Client Not Found");
                }

                // Return a successful response (200 OK) after deleting the client
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }
    }
}
