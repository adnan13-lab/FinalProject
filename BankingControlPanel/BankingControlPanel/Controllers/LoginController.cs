using BankingControlPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankingControlPanel.Controllers
{
    public class LoginController : Controller
    {
        public readonly HttpClient _httpClient;

        // Constructor to initialize HttpClient
        public LoginController(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        // GET action for the login page
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        // POST action for processing login form
        [HttpPost]
        public async Task<ActionResult> LogIn(Account account)
        {
            try
            {
                // Check if the model state is valid (i.e., proper email and password are provided)
                if (ModelState.IsValid)
                {
                    // Make a POST request to authenticate the user and get the token
                    var response = await _httpClient.PostAsJsonAsync<Account>(
                        "https://localhost:7144/api/Auth/login?email=" + account.Email + "&password=" + account.Password,
                        account
                    );

                    // If the response is successful (status code 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();

                        // If the token is not null, proceed with setting the token in cookies
                        if (!string.IsNullOrEmpty(token))
                        {
                            // Store the token in a cookie with a 30-minute expiration time
                            Response.Cookies.Append("JwtToken", token, new CookieOptions
                            {
                                Expires = DateTime.UtcNow.AddMinutes(30)
                            });

                            // Parse the token to extract the role (Admin or User)
                            var handler = new JwtSecurityTokenHandler();
                            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                            var role = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                            // Redirect user based on their role
                            if (role == "Admin")
                            {
                                return RedirectToAction("AdminDashboard", "AdminDashBoard");
                            }
                            else if (role == "User")
                            {
                                return RedirectToAction("UserDashboard", "UserDashBoard");
                            }
                            else
                            {
                                // If the role is not Admin or User, show an error and clear the token
                                ViewData["ErrorMessage"] = "You do not have the required role to access this application.";
                                Response.Cookies.Delete("JwtToken");
                            }
                        }
                        else
                        {
                            // Token is null, handle this case
                            ViewData["ErrorMessage"] = "An error occurred while processing the token. Please try again later.";
                        }
                    }
                    else
                    {
                        // If the HTTP request is not successful, show an error message
                        ViewData["ErrorMessage"] = "An error occurred while processing your request. Please try again.";
                    }
                }
                else
                {
                    // If model validation fails, show a specific error
                    ViewData["ErrorMessage"] = "Invalid login credentials. Please check your email and password.";
                }
                return View();
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and display a general error message
                ViewData["Error"] = ex.Message;
                return View();
            }
        }
    }
}
