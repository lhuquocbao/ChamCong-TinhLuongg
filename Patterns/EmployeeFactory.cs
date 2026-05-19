using ChamCongTinhLuongOOP.Models;

namespace ChamCongTinhLuongOOP.Patterns
{
    public static class EmployeeFactory
    {
        public static Employee CreateFullTimeEmployee(string id, string fullName, string department,
            decimal baseSalary, decimal allowance, decimal bonus)
        {
            return new FullTimeEmployee(id, fullName, department, baseSalary, allowance, bonus);
        }

        public static Employee CreatePartTimeEmployee(string id, string fullName, string department, decimal hourlyRate)
        {
            return new PartTimeEmployee(id, fullName, department, hourlyRate);
        }
    }
}
