using BankingControlPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace BankingControlPanel.Controllers
{
    public class AdminProfileController : Controller
    {
        string url = "https://localhost:7144/api/Client/";
        public readonly HttpClient _httpClient;

        // Constructor to initialize HttpClient
        public AdminProfileController(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        // Action to display the admin profile
        public async Task<ActionResult> Adminprofile()
        {
            try
            {
                // Retrieve the JWT token from cookies
                var token = Request.Cookies["JwtToken"];

                if (string.IsNullOrEmpty(token))
                {
                    // If token is missing or empty, return Unauthorized response
                    return Unauthorized();
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Set the Authorization header with Bearer token
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Retrieve the user ID from the token's claims
                var userId = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                // Call API to fetch the client details using userId
                var response = await _httpClient.GetFromJsonAsync<Client>(url + userId);

                // If the response contains client data, set it in ViewData for the view
                if (response != null)
                {
                    ViewData["ClientId"] = response.ClientId;
                    ViewData["FirstName"] = response.FirstName;
                    ViewData["LastName"] = response.LastName;
                    ViewData["Mobile"] = response.Mobile;
                    ViewData["Sex"] = response.Sex;
                    ViewData["ProfilePath"] = response.ProfilePath;
                    ViewData["Country"] = response.address!.Country;
                    ViewData["City"] = response.address.City;
                    ViewData["Street"] = response.address.Street;
                    ViewData["ZipCode"] = response.address.ZipCode;
                    ViewBag.Accounts = response.account;
                }
                else
                {
                    // If no client data found, set an error message in ViewData
                    ViewData["ErrorMessage"] = "No client data found.";
                }
                return View();
            }
            catch (Exception ex)
            {
                // Catch any errors that occur during the process
                ViewData["Error"] = ex.Message;
                return View();
            }
           
        }
    }
}
