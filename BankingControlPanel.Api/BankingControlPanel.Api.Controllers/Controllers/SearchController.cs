using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Controllers
{
    // Define the route for this controller (api/search) and mark it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // Declare the ISearch service which will handle the search-related logic
        public readonly ISearch _search;

        // Constructor to inject the ISearch service dependency into the controller
        public SearchController(ISearch search)
        {
            this._search = search;
        }

        // GET api/search - Fetches a list of search records, limiting the response to the top 3
        [HttpGet]
        public async Task<ActionResult> SearchList()
        {
            try
            {
                // Fetch all search data
                var response = await _search.GetSearchs();

                // Check if any of the search names contain "Not Found"
                if (response.Value!.Any(e => e.Name!.Contains("Not Found")))
                {
                    // If a search contains "Not Found", return a 404 (Not Found) response
                    return NotFound("Searchs Not Found");
                }
                else
                {
                    // Otherwise, return the top 3 search records
                    return Ok(response.Value!.Take(3));
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // GET api/search/{id} - Fetches a search record by its ID
        [HttpGet("{id}")]
        public async Task<ActionResult> SearchById(int id)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Fetch the search data by ID
                var response = await _search.GetSearchById(id);

                // If no search record is found, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Search Not Found");
                }

                // Return the search record as a successful response (200 OK)
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // POST api/search - Adds a new search record
        [HttpPost]
        public async Task<ActionResult> AddSearch(Search search)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Add the new search record using the service
                var response = await _search.AddSearch(search);

                // If the search record already exists, return a BadRequest response
                if (response == null)
                {
                    return BadRequest("Client already exists in the search database.");
                }

                // Return the added search record as a successful response (200 OK)
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a BadRequest response with the exception message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // DELETE api/search/{id} - Deletes a search record by its ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> SearchDelete(int id)
        {
            try
            {
                // Ensure the model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return BadRequest("Insert Correct Data");
                }

                // Delete the search record by ID using the service
                var response = await _search.DeleteSearch(id);

                // If the search record is not found, return a 404 (Not Found) response
                if (response == null)
                {
                    return NotFound("Search Not Found");
                }

                // Return a successful response (200 OK) after deleting the search record
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
