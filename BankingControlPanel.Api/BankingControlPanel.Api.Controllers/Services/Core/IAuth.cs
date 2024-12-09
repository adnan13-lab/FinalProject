using BankingControlPanel.Api.Models.Models;

namespace BankingControlPanel.Api.Controllers.Services.Core
{
    // IAuth Interface for user authentication
    public interface IAuth
    {
        // Method to handle user login and return a token
        public Task<string> Login(Account account);
    }
}
