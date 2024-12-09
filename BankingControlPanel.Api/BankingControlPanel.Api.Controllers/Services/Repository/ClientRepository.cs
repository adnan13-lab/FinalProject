using BankingControlPanel.Api.Controllers.Models;
using BankingControlPanel.Api.Controllers.Services.Core;
using BankingControlPanel.Api.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Api.Controllers.Services.Repository
{
    public class ClientRepository : IClient
    {
        public readonly BankingControlPanelDBcontext _bankingControlPanelDBcontext;
        public readonly IAccount _account;
        public readonly ISearch _search;


        // Constructor to initialize the database context, Account service, and Search service
        public ClientRepository(BankingControlPanelDBcontext bankingControlPanelDBcontext, IAccount account, ISearch search)
        {
            this._bankingControlPanelDBcontext = bankingControlPanelDBcontext;
            this._account = account;
            this._search = search;
        }

        // Method to retrieve all clients with their related account and address
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            var clients = await _bankingControlPanelDBcontext.Clients
                 .Include(e => e.Address)
                 .Include(e => e.Account)
                 .Where(e => e.Account!.Any())  // Ensure the client has at least one account
                 .Select(e => new Client
                 {
                     ClientId = e.ClientId,
                     FirstName = e.FirstName,
                     LastName = e.LastName,
                     PersonalId = e.PersonalId,
                     ProfilePath = e.ProfilePath,
                     Mobile = e.Mobile,
                     Sex = e.Sex,
                     Address = new Address
                     {
                         AddressId = e.Address!.AddressId,
                         Country = e.Address.Country,
                         City = e.Address.City,
                         Street = e.Address.Street,
                         ZipCode = e.Address.ZipCode,
                         ClientId = e.ClientId
                     },
                     Account = e.Account!.Select(a => new Account
                     {
                         AccountId = a.AccountId,
                         Email = a.Email,
                         Password = a.Password,
                         Role = a.Role,
                         ClientId = e.ClientId
                     }).ToList()
                 })
                 .ToListAsync();

            return clients!;
        }

        // Method to retrieve a single client by its ID
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var response = _bankingControlPanelDBcontext.Clients
                .Where(e => e.ClientId == id)
                .Include(e => e.Account)
                .Include(e => e.Address)
                .Where(e => e.Account!.Any())
                .Select(e => new Client
                {
                    ClientId = e.ClientId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PersonalId = e.PersonalId,
                    ProfilePath = e.ProfilePath,
                    Mobile = e.Mobile,
                    Sex = e.Sex,
                    Address = new Address
                    {
                        AddressId = e.Address!.AddressId,
                        Country = e.Address.Country,
                        City = e.Address.City,
                        Street = e.Address.Street,
                        ZipCode = e.Address.ZipCode,
                        ClientId = e.ClientId
                    },
                    Account = e.Account!.Select(a => new Account
                    {
                        AccountId = a.AccountId,
                        Email = a.Email,
                        Password = a.Password,
                        Role = a.Role,
                        ClientId = e.ClientId
                    }).ToList()
                }).FirstOrDefault();

            return response!;
        }

        // Method to add a new client to the database
        public async Task<ActionResult<Client>> AddClient(Client client)
        {
            var isEmailDuplicate = await _bankingControlPanelDBcontext.Clients.AnyAsync(e => e.Account!.Any(a => a.Email == client.Account!.FirstOrDefault()!.Email));
            if (isEmailDuplicate)
            {
                return null!;
            }
            else
            {
                var response = await _bankingControlPanelDBcontext.Clients.AddAsync(client);
                await _bankingControlPanelDBcontext.SaveChangesAsync();
                return response.Entity;
            }

        }

        // Method to update an existing client's information
        public async Task<ActionResult<Client>> UpdateClient(int id, Client client)
        {
            var response = await _bankingControlPanelDBcontext.Clients
                    .Include(e => e.Address)
                    .Include(e => e.Account)
                    .FirstOrDefaultAsync(e => e.ClientId == id);

            if (response !=null)
            {

                // Update basic client information
                response!.FirstName = client.FirstName;
                response.LastName = client.LastName;
                response.PersonalId = client.PersonalId;
                response.ProfilePath = client.ProfilePath;
                response.Mobile = client.Mobile;
                response.Sex = client.Sex;

                // Update address if it's provided
                if (client.Address != null)
                {
                    response!.Address!.Country = client.Address.Country;
                    response.Address.City = client.Address.City;
                    response.Address.Street = client.Address.Street;
                    response.Address.ZipCode = client.Address.ZipCode;
                }

                // Update account details if available
                var existingAccount = response!.Account!.FirstOrDefault(a => a.AccountId == response!.Account!.Select(e => e.AccountId).FirstOrDefault());
                if (existingAccount != null)
                {
                    existingAccount.Email = client!.Account!.Select(e => e.Email).FirstOrDefault();
                    existingAccount.Password = client!.Account!.Select(e => e.Password).FirstOrDefault();
                    existingAccount.Role = client!.Account!.Select(e => e.Role).FirstOrDefault();
                }
            }

            await _bankingControlPanelDBcontext.SaveChangesAsync();
            return response!;
        }

        // Method to delete a client from the database
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var response = _bankingControlPanelDBcontext.Clients
                .Where(e => e.ClientId == id)
                .Include(e => e.Account)
                .Include(e => e.Address)
                .Where(e => e.Account!.Any())
                .Select(e => new Client
                {
                    ClientId = e.ClientId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PersonalId = e.PersonalId,
                    ProfilePath = e.ProfilePath,
                    Mobile = e.Mobile,
                    Sex = e.Sex,
                    Address = new Address
                    {
                        AddressId = e.Address!.AddressId,
                        Country = e.Address.Country,
                        City = e.Address.City,
                        Street = e.Address.Street,
                        ZipCode = e.Address.ZipCode,
                        ClientId = e.ClientId
                    },
                    Account = e.Account!.Select(a => new Account
                    {
                        AccountId = a.AccountId,
                        Email = a.Email,
                        Password = a.Password,
                        Role = a.Role,
                        ClientId = e.ClientId
                    }).ToList()
                }).FirstOrDefault();


            if (response != null)
            {
                _bankingControlPanelDBcontext.Clients.Remove(response!);
                await _bankingControlPanelDBcontext.SaveChangesAsync();
            }
            return response!;
        }

        // Method to return clients with pagination
        public async Task<ActionResult<Pagination>> Pagination(int pageNum , int PageSize, string sort)
        {
            var totalClientsRecord = await _bankingControlPanelDBcontext.Clients
                .Where(e => e.Account!.All(e => e.Role == "User "))
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalClientsRecord / (double)PageSize);

            var clients = await _bankingControlPanelDBcontext.Clients
                .Include(e => e.Address)
                .Include(e => e.Account)
                .Where(e => e.Account!.All(e => e.Role == "User "))
                .ToListAsync();

            switch (sort)
            {
                case "asc":
                    clients = clients.Skip((pageNum - 1) * PageSize)
                         .Take(PageSize)
                         .OrderBy(c => c.FirstName)
                         .ToList();
                    break;

                case "desc":
                    clients = clients.Skip((pageNum - 1) * PageSize)
                         .Take(PageSize)
                         .OrderBy(c => c.FirstName)
                         .OrderByDescending(c => c.FirstName)
                         .ToList();
                    break;

                default:
                    clients = clients
                         .Skip((pageNum - 1) * PageSize)
                         .Take(PageSize)
                         .ToList();
                    break;
            }
            var response = new Pagination
            {
                TotalRecords = totalClientsRecord,
                TotalPages = totalPages,
                CurrentPage = pageNum,
                PageSize = PageSize,
                Clients = clients
            };

            return response;
        }

        // Method to filter clients based on first name and add a search entry
        public async Task<ActionResult<List<Client>>> FilterClient(string name)
        {
            var client = await _bankingControlPanelDBcontext.Clients
                .Where(e => e.FirstName == name)
                .Include(e => e.Address)
                .Include(e => e.Account)
                .Where(e => e.Account!.Any())
                .Select(e => new Client
                {
                    ClientId = e.ClientId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PersonalId = e.PersonalId,
                    ProfilePath = e.ProfilePath,
                    Mobile = e.Mobile,
                    Sex = e.Sex,
                    Address = new Address
                    {
                        AddressId = e.Address!.AddressId,
                        Country = e.Address.Country,
                        City = e.Address.City,
                        Street = e.Address.Street,
                        ZipCode = e.Address.ZipCode,
                        ClientId = e.ClientId
                    },
                    Account = e.Account!.Select(a => new Account
                    {
                        AccountId = a.AccountId,
                        Email = a.Email,
                        Password = a.Password,
                        Role = a.Role,
                        ClientId = e.ClientId
                    }).ToList()
                })
                .ToListAsync();

            // Log search results into Search table
            if (client != null)
            {
                var searchs = new Search
                {
                    Name = client.Select(e => e.FirstName).FirstOrDefault(),
                    ClientId = client.Select(e => e.ClientId).FirstOrDefault(),
                };
                await _search.AddSearch(searchs);
            }

            return client!;
        }
    }
}


//.Select(e => new Client
//{
//    ClientId = e.ClientId,
//    FirstName = e.FirstName,
//    LastName = e.LastName,
//    PersonalId = e.PersonalId,
//    ProfilePath = e.ProfilePath,
//    Mobile = e.Mobile,
//    Sex = e.Sex,
//    Address = new Address
//    {
//        AddressId = e.Address!.AddressId,
//        Country = e.Address.Country,
//        City = e.Address.City,
//        Street = e.Address.Street,
//        ZipCode = e.Address.ZipCode,
//        ClientId = e.ClientId
//    },
//    Account = e.Account!.Select(a => new Account
//    {
//        AccountId = a.AccountId,
//        Email = a.Email,
//        Password = a.Password,
//        Role = a.Role,
//        ClientId = e.ClientId
//    }).ToList()
//})