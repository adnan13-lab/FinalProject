using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Services.Core
{
    // IAccount Interface
    public interface IAccount
    {
        // Asynchronously retrieves a list of all accounts
        public Task<ActionResult<List<Account>>> GetAccounts();

        // Asynchronously retrieves an account by its ID
        public Task<ActionResult<Account>> GetAccountById(int id);

        // Asynchronously Add New Account
        public Task<ActionResult<Account>> AddAccount(Account account);

        // Asynchronously updates an existing account by ID
        public Task<ActionResult<Account>> UpdateAccount(int id, Account account);

        // Asynchronously deletes an account by ID
        public Task<ActionResult<Account>> DeleteAccount(int id);
    }
}
