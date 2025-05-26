using ChequePrintWebApp.Data;
using ChequePrintWebApp.Helper;
using ChequePrintWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;

namespace ChequePrintWebApp.Controllers
{
    public class ChequeController : Controller
    {

        private static List<Employee> _employees;
        private static List<ChequeHistory> _history;


        public ChequeController(IWebHostEnvironment env)
        {
            if (_employees == null)
            {
                var path = Path.Combine(env.WebRootPath, "files", "Employees.xlsx");
                _employees = ExcelHelper.LoadEmployees(path);
            }
            if (_history == null)
            {
                _history = JsonDataHelper.LoadHistory();
            }
        }


        [HttpGet]
            public IActionResult Index()
            {
                return View(new ChequeViewModel());
            }


        [HttpPost, ActionName("Index")]
        public IActionResult Index(string personalNumber, decimal amount)
        {
            var emp = _employees.FirstOrDefault(e => e.PersonalNumber == personalNumber);
            var model = new ChequeViewModel
            {
                PersonalNumber = personalNumber,
                Amount = amount
            };
            if (emp == null)
            {
            
                ModelState.AddModelError("PersonalNumber", "Employee not found.");
                return View(model);
            }

            var model1 = new ChequeViewModel
            {
                Name = emp.Name,
                PersonalNumber = emp.PersonalNumber,
                BankName = emp.BankName,
                BankAccountNumber = emp.BankAccountNumber,
                Amount = amount,
                AmountInWords = NumberToWordsConverter.ConvertToWords(amount),
                CurrentDate = DateTime.Now
            };

            return View("PrintPreview", model1);
        }

        //---Get information from PF Calculator Page---//
        [HttpGet]
        public IActionResult CreateFromPF(string personalNumber, decimal? amount)
        {
            var model = new ChequeViewModel();

            if (!string.IsNullOrWhiteSpace(personalNumber))
            {
                var emp = _employees.FirstOrDefault(e => e.PersonalNumber == personalNumber);
                if (emp != null)
                {
                    model.PersonalNumber = emp.PersonalNumber;
                    model.Name = emp.Name;
                    model.BankName = emp.BankName;
                    model.BankAccountNumber = emp.BankAccountNumber;
                }
            }

            if (amount.HasValue)
            {
                model.Amount = amount.Value;
                model.AmountInWords = NumberToWordsConverter.ConvertToWords(amount.Value);
                model.CurrentDate = DateTime.Now;
            }

            return View("Index", model);
        }

        //---For Record of Cheque History---//
        [HttpPost]
        public IActionResult SaveCheque(ChequeViewModel model)
        {
            var emp = _employees.FirstOrDefault(e => e.PersonalNumber == model.PersonalNumber);

            if (emp == null)
            {
                ModelState.AddModelError("PersonalNumber", "Employee not found.");
                return View("Index", model); // show form again with error
            }

            // Prepare full model
            model.Name = emp.Name;
            model.BankName = emp.BankName;
            model.BankAccountNumber = emp.BankAccountNumber;
            model.AmountInWords = NumberToWordsConverter.ConvertToWords(model.Amount);
            model.CurrentDate = DateTime.Now;

            // Save to history
            _history.Add(new ChequeHistory
            {
                Name = model.Name,
                PersonalNumber = model.PersonalNumber,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccountNumber,
                Amount = model.Amount,
                AmountInWords = model.AmountInWords,
                Date = model.CurrentDate
            });
            JsonDataHelper.SaveHistory(_history);

            // Show print view
            return View("PrintPreview", model);
        }




        public IActionResult History(string search, DateTime? fromDate, DateTime? toDate)
        {
            var history = JsonDataHelper.LoadHistory(); // Or DB if using EF

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                history = history
                    .Where(h => h.PersonalNumber.ToLower().Contains(search) || h.Name.ToLower().Contains(search))
                    .ToList();
            }

            if (fromDate.HasValue)
                history = history.Where(h => h.Date.Date >= fromDate.Value.Date).ToList();

            if (toDate.HasValue)
                history = history.Where(h => h.Date.Date <= toDate.Value.Date).ToList();

            return View(history.OrderByDescending(h => h.Date).ToList());
        }



        //---Export to Excel--//
        [HttpGet]
        public IActionResult ExportToExcel()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Cheque History");

                // Header row
                worksheet.Cells[1, 1].Value = "Date";
                worksheet.Cells[1, 2].Value = "Personal Number";
                worksheet.Cells[1, 3].Value = "Name";
                worksheet.Cells[1, 4].Value = "Bank";
                worksheet.Cells[1, 5].Value = "Account No.";
                worksheet.Cells[1, 6].Value = "Amount";
             

                int row = 2;
                foreach (var item in _history.OrderByDescending(h => h.Date))
                {
                    worksheet.Cells[row, 1].Value = item.Date.ToString("dd-MMM-yyyy");
                    worksheet.Cells[row, 2].Value = item.PersonalNumber;
                    worksheet.Cells[row, 3].Value = item.Name;
                    worksheet.Cells[row, 4].Value = item.BankName;
                    worksheet.Cells[row, 5].Value = item.BankAccountNumber;
                    worksheet.Cells[row, 6].Value = item.Amount;
                    row++;
                }

                //worksheet.Cells.AutoFitColumns();

                var excelData = package.GetAsByteArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ChequeHistory.xlsx");
            }
        }




    }

}

