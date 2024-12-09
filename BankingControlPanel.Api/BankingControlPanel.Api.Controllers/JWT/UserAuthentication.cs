using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingControlPanel.Api.Controllers.JWT
{
    public class UserAuthentication
    {
        // Declare IConfiguration to access app settings (for JWT secrets, issuer, and audience)
        private readonly IConfiguration _configuration;

        // Constructor to inject IConfiguration dependency
        public UserAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Method to generate a JWT token based on username, email, and role
        public string Authentication(string userName,int id, string email, string role)
        {
            try
            {
                // Create a list of claims to embed in the JWT token
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),  // User's name claim
                    new Claim(ClaimTypes.NameIdentifier,id.ToString()), // User's id claim
                    new Claim(ClaimTypes.Email, email),    // User's email claim
                    new Claim(ClaimTypes.Role, role),      // User's role claim
                };

                // Generate a symmetric security key from the secret key defined in the configuration (appsettings.json)
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));

                // Define the signing credentials using the key and HMAC SHA256 algorithm
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create a JWT token with the provided claims, issuer, audience, and expiration time
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIssuer"],     // Set the token's issuer (usually the server)
                    audience: _configuration["Jwt:ValidAudience"], // Set the token's audience (who the token is for)
                    claims: claims,                                // Claims to include in the token
                    expires: DateTime.Now.AddMinutes(30),          // Set the expiration time (e.g., 30 minutes)
                    signingCredentials: creds                      // Signing credentials to secure the token
                );

                // Return the generated token as a string
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                // Return a custom message in case of an exception during token generation
                return "Message: " + ex.Message;
            }
        }
    }
}
