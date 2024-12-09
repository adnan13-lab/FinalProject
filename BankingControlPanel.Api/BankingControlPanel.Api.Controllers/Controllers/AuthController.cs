using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Controllers
{
    // ApiController attribute ensures automatic model validation and binding of HTTP requests
    [ApiController]
    // Route to map this controller to the api/auth endpoint
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Declare the IAuth service which will handle the authentication logic
        public readonly IAuth _auth;

        // Constructor to inject the IAuth service dependency into the controller
        public AuthController(IAuth auth)
        {
            this._auth = auth;
        }

        // POST api/auth/login - Login endpoint for user authentication
        [HttpPost("login")]
        public async Task<ActionResult> Login(Account account)
        {
            try
            {
                // Check if the model state is valid before processing
                if (!ModelState.IsValid)
                {
                    // If model state is invalid, return a 400 (Bad Request) response
                    return BadRequest("Insert Correct Data");
                }

                // Call the service to perform login with the provided email and password
                var response = await _auth.Login(account);


                // If the login was successful (i.e., response is not null), return a successful response with the token or user info
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    // If login fails, return a 401 Unauthorized response with a message
                    return Unauthorized("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs during the authentication process, return a 400 (Bad Request) response with the error message
                return BadRequest("Message: " + ex.Message);
            }
        }
    }
}
