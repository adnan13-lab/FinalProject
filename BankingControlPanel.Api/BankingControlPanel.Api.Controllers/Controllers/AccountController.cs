using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Api.Controllers.Controllers
{
    // The ApiController attribute automatically applies model validation and binds HTTP requests
    [ApiController]
    // Route to map this controller to the api/account endpoint
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        // Declare the IAccount service which will handle the business logic for account operations
        public IAccount _account;

        // Constructor to inject the IAccount service dependency
        public AccountController(IAccount account)
        {
            this._account = account;
        }

        // GET api/account - Get a list of all accounts
        [HttpGet]
        public async Task<ActionResult> AccountList()
        {
            try
            {
                // Call the service to get a list of accounts
                var response = await _account.GetAccounts();

                // If no accounts are found, return a 404 (Not Found) response
                if (response!.Value!.Count! == 0)
                {
                    return NotFound("Account List Is Empty");
                }

                // Return a successful response with the list of accounts
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 400 (Bad Request) with the error message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // GET api/account/{id} - Get an account by its ID
        [HttpGet("{id}")]
        public async Task<ActionResult> AccountById(int id)
        {
            try
            {
                // Check if the model state is valid before processing
                if (!ModelState.IsValid)
                {
                    // If model state is invalid, return a 400 (Bad Request) response
                    return BadRequest("Insert Correct Data");
                }

                // Call the service to get an account by its ID
                var response = await _account.GetAccountById(id);

                // If no account is found with the provided ID, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Account Not Found");
                }

                // Return the account details in a successful response
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 400 (Bad Request) with the error message
                return BadRequest("Message: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Addaccount(Account account)
        {
            try
            {   
                // Check if the model state is valid before processing
                if (!ModelState.IsValid)
                {
                    // If model state is invalid, return a 400 (Bad Request) response
                    return BadRequest("Insert Correct Data");
                }
                // Call the service to add the account 
                var response = await _account.AddAccount(account);

                // If the account data is not correct, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return BadRequest("Incorrect Formate");
                }

                // Return a successful response with the new account 
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 400 (Bad Request) with the error message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // PUT api/account/{id} - Update an account's information
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccount(int id, Account account)
        {
            try
            {
                // Check if the model state is valid before processing
                if (!ModelState.IsValid)
                {
                    // If model state is invalid, return a 400 (Bad Request) response
                    return BadRequest("Insert Correct Data");
                }

                // Call the service to update the account with the provided ID
                var response = await _account.UpdateAccount(id, account);

                // If the account was not found, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Account Not Found");
                }

                // Return a successful response with the updated account details
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 400 (Bad Request) with the error message
                return BadRequest("Message: " + ex.Message);
            }
        }

        // DELETE api/account/{id} - Delete an account by its ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            try
            {
                // Check if the model state is valid before processing
                if (!ModelState.IsValid)
                {
                    // If model state is invalid, return a 400 (Bad Request) response
                    return BadRequest("Insert Correct Data");
                }

                // Call the service to delete the account with the provided ID
                var response = await _account.DeleteAccount(id);

                // If the account was not found, return a 404 (Not Found) response
                if (response.Value == null)
                {
                    return NotFound("Account Not Found");
                }

                // Return a successful response confirming the account was deleted
                return Ok(response.Value);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 400 (Bad Request) with the error message
                return BadRequest("Message: " + ex.Message);
            }
        }
    }
}
