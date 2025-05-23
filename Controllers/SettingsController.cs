using ChequePrintWebApp.Data;
using ChequePrintWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChequePrintWebApp.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View(LayoutSettings.Positions);
        }

        [HttpPost]
        public IActionResult Index(List<PrintPosition> positions)
        {
            // Save positions (future: to file/db)
            LayoutSettings.Positions = positions;
            ViewBag.Message = "Settings updated successfully!";
            return View(positions);
        }
    }

}
