using BankingControlPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Controllers
{
    public class RegistrationController : Controller
    {
        // API endpoint for client registration
        string url = "https://localhost:7144/api/Client";
        public readonly HttpClient _httpClient;
        public readonly IWebHostEnvironment _hostingEnvironment;

        // Constructor to initialize HttpClient and IWebHostEnvironment
        public RegistrationController(HttpClient httpClient, IWebHostEnvironment hostingEnvironment)
        {
            this._httpClient = httpClient;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Displays the registration view
        [HttpGet]
        public IActionResult Registrations()
        {
            // Return the registration view
            return View();
        }

        // POST: Handles the registration of a new client
        [HttpPost]
        [Route("Registration/Registrations/{AddByAdmin}")]
        public async Task<ActionResult> Registrations(Registration registration, string AddByAdmin)
        {
            try
            {
                // Check if the model state is valid
                if (ModelState.IsValid)
                {
                    // Check if an image is uploaded
                    if (registration.Image == null)
                    {
                        // Use a default image if no image is uploaded
                        var imageUrl = Url.Content("~/images/images.png");

                        // Create a new client object
                        var client = new Client
                        {
                            FirstName = registration.FirstName,
                            LastName = registration.LastName,
                            PersonalId = registration.PersonalId,
                            ProfilePath = imageUrl.ToString(),
                            Mobile = registration.Mobile,
                            Sex = registration.Sex,
                            address = new Address
                            {
                                Country = registration.Country,
                                City = registration.City,
                                Street = registration.Street,
                                ZipCode = registration.ZipCode
                            },
                            account = new List<Account>
                            {
                                new Account
                                {
                                    Email = registration.Email,
                                    Password = registration.Password,
                                    Role = registration.Role
                                }
                            }
                        };

                        // Send the client data to the API
                        var response = await _httpClient.PostAsJsonAsync(url, client);

                        // Check if the response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Set a success message
                            TempData["AddMessage"] = "Client Added Successfully";

                            // Redirect based on whether the registration was done by an admin
                            if (AddByAdmin == "true")
                            {
                                return RedirectToAction("AdminDashboard", "AdminDashBoard");
                            }
                            else
                            {
                                return RedirectToAction("LogIn", "Login");
                            }
                        }
                    }
                    else
                    {
                        // Handle file upload
                        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + registration.Image!.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the uploaded image to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await registration.Image.CopyToAsync(fileStream);
                        }

                        // Get the URL of the uploaded image
                        var imageUrl = Url.Content("~/images/" + uniqueFileName);

                        // Create a new client object with the uploaded image
                        var client = new Client
                        {
                            FirstName = registration.FirstName,
                            LastName = registration.LastName,
                            PersonalId = registration.PersonalId,
                            ProfilePath = imageUrl,
                            Mobile = registration.Mobile,
                            Sex = registration.Sex,
                            address = new Address
                            {
                                Country = registration.Country,
                                City = registration.City,
                                Street = registration.Street,
                                ZipCode = registration.ZipCode
                            },
                            account = new List<Account>
                            {
                                new Account
                                {
                                    Email = registration.Email,
                                    Password = registration.Password,
                                    Role = registration.Role
                                }
                            }
                        };

                        // Send the client data to the API
                        var response = await _httpClient.PostAsJsonAsync(url, client);

                        // Check if the response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Set a success message
                            TempData["AddMessage"] = "Client Added Successfully";

                            // Redirect based on whether the registration was done by an admin
                            if (AddByAdmin == "true")
                            {
                                return RedirectToAction("AdminDashboard", "AdminDashBoard");
                            }
                            else
                            {
                                return RedirectToAction("LogIn", "Login");
                            }
                        }
                    }
                }
                return View(registration);
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and display a general error message
                ViewData["Error"] = ex.Message;
                return View(registration);
            }
        }
    }
}