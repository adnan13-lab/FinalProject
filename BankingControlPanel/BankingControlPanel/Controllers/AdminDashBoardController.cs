using BankingControlPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BankingControlPanel.Controllers
{
    public class AdminDashBoardController : Controller
    {
        public readonly HttpClient _httpClient;

        // Define API endpoint URLs
        public string SearchUrl = "https://localhost:7144/api/Client/FilterClient?name=";
        public string Searchs = "https://localhost:7144/api/Search";

        // Constructor to initialize HttpClient
        public AdminDashBoardController(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        // Action to load the admin dashboard with pagination and client data
        [HttpGet]
        public async Task<ActionResult> AdminDashboard(int pageNum = 1, int pageSize = 10, string sort = "null")
        {
            try
            {
                // Retrieve the JWT token from cookies
                var token = Request.Cookies["JwtToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Unauthorized if token is null or empty
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }

                // Set Authorization header with the token for API requests
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Construct the URL for client pagination
                string url = $"https://localhost:7144/api/Client/Pagination?pageNum={pageNum}&PageSize={pageSize}&sort={sort}";
                var response = await _httpClient.GetFromJsonAsync<Pagination>(url);

                // Check if client data is available
                if (response != null && response.Clients!.Count > 0)
                {
                    // Fetch search history
                    await GetSearchs(Searchs);

                    var Page = new Pagination 
                     { 
                        TotalRecords = response.TotalRecords,
                        TotalPages = response.TotalPages,
                        CurrentPage = response.CurrentPage,
                        PageSize = response.PageSize,
                        Clients = response.Clients,
                     };

                    TempData["AdminMessage"] = "Client Register By Admin";
                    return View(Page);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // Action to filter clients based on a name search
        [HttpGet]
        public async Task<ActionResult> FilterClients(string name)
        {
            try
            {
                // Retrieve the JWT token from cookies
                var token = Request.Cookies["JwtToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Unauthorized if token is null or empty
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }

                // Set Authorization header with the token for API requests
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Make the API call to filter clients by name
                var response = await _httpClient.GetFromJsonAsync<List<Client>>(SearchUrl + name);

                // Check if any clients were returned
                if (response != null && response!.Count > 0)
                {
                    // Filter and map the client data for display
                    var filteredPagination = new Pagination
                    {
                        TotalRecords = response.Count, 
                        TotalPages = 1, 
                        CurrentPage = 1, 
                        PageSize = response.Count, 
                        Clients = response.Where(e => e.account != null)
                            .Select(e => new Client
                            {
                                ClientId = e.ClientId,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                PersonalId = e.PersonalId,
                                ProfilePath = e.ProfilePath,
                                Mobile = e.Mobile,
                                Sex = e.Sex,
                                address = new Address
                                {
                                    Country = e.address!.Country,
                                    City = e.address.City,
                                    Street = e.address.Street,
                                    ZipCode = e.address.ZipCode
                                },
                                account = e.account!
                                    .Select(account => new Account
                                    {
                                        Email = account.Email,
                                        Password = account.Password,
                                        Role = account.Role
                                    }).ToList()
                            }).ToList()
                       };



                    // Fetch search history
                    await GetSearchs(Searchs);
                    return View("AdminDashBoard",filteredPagination);
                }
                else
                {
                    ViewData["Error"] = "Not Found";
                    // If no results, redirect to an error page
                    return RedirectToAction("ErrorDialog", "ErrorDialogBox");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorDialog", "ErrorDialogBox");
                
            }
        }

        // Action to fetch search history
        [HttpGet]
        public async Task<ActionResult<Searchs>> GetSearchs(string url)
        {
            try
            {
                // Retrieve the JWT token from cookies
                var token = Request.Cookies["JwtToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Unauthorized if token is null or empty
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }

                // Set Authorization header with the token for API requests
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Make the API call to fetch search history
                var response = await _httpClient.GetFromJsonAsync<List<Searchs>>(url);

                // Check if search history exists
                if (response != null && response.Count > 0)
                {
                    // Pass search history to the view
                    ViewData["SearchHistory"] = response;
                }

                return View("AdminDashBoard");
            }
            catch (Exception ex)
            {
               ViewData["Error"] = ex.Message;
               return View("AdminDashBoard");
            }
        }
    }
}
