using ChamCongTinhLuongOOP.Utilities;

namespace ChamCongTinhLuongOOP.Models
{
    public class PayrollResult
    {
        public string EmployeeId { get; private set; }
        public string FullName { get; private set; }
        public string EmployeeType { get; private set; }
        public decimal GrossSalary { get; private set; }
        public decimal Tax { get; private set; }
        public decimal NetSalary { get; private set; }
        public AttendanceRecord Attendance { get; private set; }

        public PayrollResult(string employeeId, string fullName, string employeeType,
            decimal grossSalary, decimal tax, decimal netSalary, AttendanceRecord attendance)
        {
            EmployeeId = employeeId;
            FullName = fullName;
            EmployeeType = employeeType;
            GrossSalary = grossSalary;
            Tax = tax;
            NetSalary = netSalary;
            Attendance = attendance;
        }

        public string ToReportLine()
        {
            return string.Format(
                "{0,-10} | {1,-25} | {2,-10} | Gross: {3,15} | Thuế: {4,15} | Thực nhận: {5,15}",
                EmployeeId,
                FullName,
                EmployeeType,
                MoneyFormatter.Format(GrossSalary),
                MoneyFormatter.Format(Tax),
                MoneyFormatter.Format(NetSalary)
            );
        }
    }
}
