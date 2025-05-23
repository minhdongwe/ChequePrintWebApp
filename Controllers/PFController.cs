using ChequePrintWebApp.Helper;
using ChequePrintWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChequePrintWebApp.Controllers
{
    public class PFController : Controller
    {
        private static List<PFViewModel> _pfData;

        public PFController(IWebHostEnvironment env)
        {
            if (_pfData == null)
            {
                var path = Path.Combine(env.WebRootPath, "files", "PFFile1.xlsx");

                Console.WriteLine("Looking for file at: " + path);

                if (System.IO.File.Exists(path))
                {
                    _pfData = PFExcelHelper.LoadPFData(path);
                    Console.WriteLine("✅ PF data loaded: " + _pfData.Count);
                }
                else
                {
                    Console.WriteLine("❌ PFFile.xlsx NOT found at: " + path);
                    _pfData = new List<PFViewModel>(); // prevent null crash
                }
            }
        }




        [HttpGet]
        public IActionResult Index() => View(new PFViewModel());

        [HttpPost]
        public IActionResult Index(PFViewModel inputModel)
        {
            var emp = _pfData.FirstOrDefault(e => e.PersonalNumber == inputModel.PersonalNumber);

            if (emp == null)
            {
                ModelState.AddModelError("PersonalNumber", "Personal Number not found.");
                return View(new PFViewModel());
            }

            // Load data from Excel
            var model = new PFViewModel
            {
                PersonalNumber = emp.PersonalNumber,
                EmployeeName = emp.EmployeeName,
                PFMemberContribution = emp.PFMemberContribution,
                PFEmployerContribution = emp.PFEmployerContribution,
                PayrollDeduction = inputModel.PayrollDeduction
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetByPersonalNumber(string personalNumber)
        {
            if (_pfData == null || !_pfData.Any())
            {
                Console.WriteLine("❌ _pfData is NULL or EMPTY");
                return Json(null);
            }

            var emp = _pfData.FirstOrDefault(e => e.PersonalNumber == personalNumber);
            return emp == null ? Json(null) : Json(new
            {
                emp.EmployeeName,
                emp.PFMemberContribution,
                emp.PFEmployerContribution,
                Total = emp.PFMemberContribution + emp.PFEmployerContribution
            });
        }


    }

}
