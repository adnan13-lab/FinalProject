using BankingControlPanel.Api.Controllers.JWT;
using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;

namespace BankingControlPanel.Api.Controllers.Services.Repository
{
    // Repository class for handling authentication-related database operations
    public class AuthRepository : IAuth
    {
        // Declare readonly fields for DbContext and JWT authentication service
        public readonly BankingControlPanelDBcontext _bankingControlPanelDBcontext;
        public readonly UserAuthentication _userAuthentication;

        // Constructor to inject the DbContext and UserAuthentication services
        public AuthRepository(BankingControlPanelDBcontext bankingControlPanelDBcontext, UserAuthentication userAuthentication)
        {
            this._bankingControlPanelDBcontext = bankingControlPanelDBcontext;
            this._userAuthentication = userAuthentication;
        }

        // Method to authenticate user based on email and password
        public async Task<string> Login(Account account)
        {
            try
            {
                // Retrieve the account matching the provided email and password
                var AccountResponse = _bankingControlPanelDBcontext.Accounts
                    .Where(e => e.Email == account.Email && e.Password == account.Password && e.Role == account.Role)
                    .FirstOrDefault();

                // If no account found, return an empty string
                if (AccountResponse == null)
                {
                    return null!;
                }

                // Retrieve the associated client's first name based on the account's clientId
                var ClientResponse = _bankingControlPanelDBcontext.Clients
                    .Where(e => e.ClientId == AccountResponse.ClientId)
                    .Select(e => e.FirstName)
                    .FirstOrDefault();

                // If client information and account details are valid, generate a JWT token
                if (ClientResponse != null && AccountResponse.Email != null && AccountResponse.Password != null)
                {
                    // Generate the token using the UserAuthentication service
                    var token = _userAuthentication.Authentication(ClientResponse, AccountResponse.ClientId, AccountResponse.Email, AccountResponse!.Role!);
                    return token;
                }
                else
                {
                    // Return an empty string if client or account information is missing
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                // Return an empty string or log error as needed if an exception occurs
                // For now, returning an empty string in case of an exception
                return null!;
            }
        }
    }
}
