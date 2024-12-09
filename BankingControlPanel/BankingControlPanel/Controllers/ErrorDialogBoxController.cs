using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanel.Controllers
{
    public class ErrorDialogBoxController : Controller
    {
        // Show Error Dialog If email and password Not found
        public IActionResult ErrorDialog()
        {
            return View();
        }
    }
}
