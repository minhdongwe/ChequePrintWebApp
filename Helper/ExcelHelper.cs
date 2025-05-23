using ChequePrintWebApp.Models;
using OfficeOpenXml;

public static class ExcelHelper
{
    public static List<Employee> LoadEmployees(string path)
    {
        var employees = new List<Employee>();

        // ✅ EPPlus v8 license setup
       // ExcelPackage.License = new OfficeOpenXml.License.LicenseProvider().LoadLicenseText("noncommercial");
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(new FileInfo(path)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                employees.Add(new Employee
                {
                    PersonalNumber = worksheet.Cells[row, 1].Text,
                    Name = worksheet.Cells[row, 2].Text,
                    BankName = worksheet.Cells[row, 3].Text,
                    BankAccountNumber = worksheet.Cells[row, 4].Text
                });
            }
        }

        return employees;
    }
}
