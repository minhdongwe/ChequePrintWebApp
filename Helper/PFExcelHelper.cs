using ChequePrintWebApp.Models;
using OfficeOpenXml;

namespace ChequePrintWebApp.Helper
{
    public static class PFExcelHelper
    {
        public static List<PFViewModel> LoadPFData(string path)
        {
            var list = new List<PFViewModel>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var sheet = package.Workbook.Worksheets[0];
                int rowCount = sheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    list.Add(new PFViewModel
                    {
                        PersonalNumber = sheet.Cells[row, 1].Text,
                        EmployeeName = sheet.Cells[row, 2].Text,
                        PFMemberContribution = decimal.Parse(sheet.Cells[row, 3].Text),
                        PFEmployerContribution = decimal.Parse(sheet.Cells[row, 4].Text)
                    });
                }
            }

            return list;
        }
    }

}
