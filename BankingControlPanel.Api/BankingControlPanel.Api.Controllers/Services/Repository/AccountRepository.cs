using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Api.Controllers.Services.Repository
{
    // Repository class for handling Account-related database operations
    public class AccountRepository : IAccount
    {
        // Declare a readonly field for the DbContext to interact with the database
        public readonly BankingControlPanelDBcontext _bankingControlPanelDBcontext;

        // Constructor to inject the DbContext instance
        public AccountRepository(BankingControlPanelDBcontext bankingControlPanelDBcontext)
        {
            this._bankingControlPanelDBcontext = bankingControlPanelDBcontext;
        }

        // Get a list of all accounts from the database
        public async Task<ActionResult<List<Account>>> GetAccounts()
        {
            try
            {
                // Fetch all accounts asynchronously using Entity Framework
                var response = await _bankingControlPanelDBcontext.Accounts.ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                // Return a BadRequest response in case of errors
                return new BadRequestObjectResult("Error: " + ex.Message);
            }
        }

        // Get a specific account by its ID
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            // Find the account by its ID
            var response = await _bankingControlPanelDBcontext.Accounts.FindAsync(id);

            // If account not found, return null (can be handled in the controller layer)
            return response!;
        }

        public async Task<ActionResult<Account>> AddAccount(Account account)
        {
            // Add the new account asynchronously to the database context
            var response = await _bankingControlPanelDBcontext.Accounts.AddAsync(account);

            // Save the changes to persist the new account in the database
            await _bankingControlPanelDBcontext.SaveChangesAsync();

            // Check if the account was successfully added; if not, return a BadRequest response
            if (response == null)
            {
                return new BadRequestObjectResult("Incorrect Format");
            }

            // Return the added account entity as the response
            return response.Entity;
        }


        // Update an existing account by its ID
        public async Task<ActionResult<Account>> UpdateAccount(int id, Account account)
        {
            // Find the account by its ID
            var response = await _bankingControlPanelDBcontext.Accounts.FindAsync(id);

            // If account is not found, handle it (return a NotFound response)
            if (response == null)
            {
                return new NotFoundObjectResult("Account not found");
            }

            // Update the account's properties with the new data
            response.Email = account.Email;
            response.Password = account.Password;
            response.Role = account.Role;

            // Save the changes to the database asynchronously
            await _bankingControlPanelDBcontext.SaveChangesAsync();
            return response;
        }

        // Delete an account by its ID
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            // Find the account by its ID
            var response = await _bankingControlPanelDBcontext.Accounts.FindAsync(id);

            // If account is not found, handle it (return a NotFound response)
            if (response == null)
            {
                return new NotFoundObjectResult("Account not found");
            }

            // Remove the account from the database
            _bankingControlPanelDBcontext.Accounts.Remove(response);

            // Save the changes to the database asynchronously
            await _bankingControlPanelDBcontext.SaveChangesAsync();

            // Return the deleted account (could also return a success message if preferred)
            return response;
        }
    }
}
