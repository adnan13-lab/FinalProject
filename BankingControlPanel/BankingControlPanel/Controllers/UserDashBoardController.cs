using BankingControlPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BankingControlPanel.Controllers
{
    public class UserDashBoardController : Controller
    {
        // HttpClient instance to make API requests
        public readonly HttpClient _httpClient;

        // URL to fetch client data
        string url = "https://localhost:7144/api/Client";

        // Constructor to initialize HttpClient
        public UserDashBoardController(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        // GET action to load the user dashboard
        [HttpGet]
        public async Task<ActionResult> UserDashboard()
        {
            // Retrieve the JWT token from cookies
            var token = Request.Cookies["JwtToken"];

            // If the token is not available, redirect to the login page
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("LogIn", "Login");
            }

            try
            {
                // Set the Authorization header with the Bearer token
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Fetch the list of clients from the API
                var response = await _httpClient.GetFromJsonAsync<List<Client>>(url);

                // Check if the response contains any client data
                if (response != null && response.Count != 0)
                {
                    // Parse the JWT token to extract the email claim
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                    var userEmail = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                    // Find the client whose email matches the user email
                    var client = response.FirstOrDefault(e => e.account != null && e.account.Any(a => a.Email == userEmail));

                    // If client data is found, populate the ViewData with the client's information
                    if (client != null)
                    {
                        var firstAccount = client.account!.FirstOrDefault(); // Retrieve the first account associated with the client

                        // Set client details in ViewData
                        ViewData["ClientId"] = client.ClientId;
                        ViewData["FirstName"] = client.FirstName;
                        ViewData["LastName"] = client.LastName;
                        ViewData["Email"] = firstAccount?.Email;
                        ViewData["Mobile"] = client.Mobile;
                        ViewData["Sex"] = client.Sex;
                        ViewData["ProfilePath"] = client.ProfilePath;
                        ViewData["Country"] = client.address!.Country;
                        ViewData["City"] = client.address.City;
                        ViewData["Street"] = client.address.Street;
                        ViewData["ZipCode"] = client.address.ZipCode;
                        ViewData["AccountId"] = firstAccount?.AccountId;
                        ViewData["AccountEmail"] = firstAccount?.Email;
                        ViewData["AccountPassword"] = firstAccount?.Password;
                        ViewData["AccountRole"] = firstAccount?.Role;
                    }
                }
                else
                {
                    // If no client data is found, display an error message
                    ViewData["ErrorMessage"] = "No client data found.";
                }
                return View();
            }
            catch (Exception ex)
            {
                // Display a general error message to the user
                ViewData["Error"] = ex.Message;
                return View();
            }
        }
    }
}
