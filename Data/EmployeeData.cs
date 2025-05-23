using ChequePrintWebApp.Models;

namespace ChequePrintWebApp.Data
{
    public static class EmployeeData
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
        {
            new Employee
            {
                PersonalNumber = "12345",
                Name = "Ali Mohtisham",
                BankName = "HBL",
                BankAccountNumber = "01234567890123"
            },
            new Employee
            {
                PersonalNumber = "67890",
                Name = "Ahmed Khan",
                BankName = "UBL",
                BankAccountNumber = "09876543210987"
            }
        };
        }
    }

}
