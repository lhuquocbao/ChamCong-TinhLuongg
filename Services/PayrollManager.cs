using System;
using System.Collections.Generic;
using System.Linq;
using ChamCongTinhLuongOOP.Models;
using ChamCongTinhLuongOOP.Patterns;

namespace ChamCongTinhLuongOOP.Services
{
    public sealed class PayrollManager
    {
        private static readonly PayrollManager _instance = new PayrollManager();
        private readonly List<Employee> _employees;

        public static PayrollManager Instance
        {
            get { return _instance; }
        }

        private PayrollManager()
        {
            _employees = new List<Employee>();
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            if (FindEmployeeById(employee.Id) != null)
            {
                throw new InvalidOperationException("Mã nhân viên đã tồn tại.");
            }

            _employees.Add(employee);
        }

        public bool RemoveEmployee(string id)
        {
            Employee employee = FindEmployeeById(id);
            if (employee == null)
            {
                return false;
            }

            _employees.Remove(employee);
            return true;
        }

        public Employee FindEmployeeById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            string normalizedId = id.Trim().ToUpper();
            return _employees.FirstOrDefault(e => e.Id == normalizedId);
        }

        public List<Employee> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<Employee>();
            }

            string normalized = keyword.Trim().ToLower();
            return _employees
                .Where(e => e.FullName.ToLower().Contains(normalized))
                .ToList();
        }

        public List<PayrollResult> CalculateAllPayrolls(ITaxStrategy taxStrategy)
        {
            List<PayrollResult> results = new List<PayrollResult>();
            foreach (Employee employee in _employees)
            {
                results.Add(employee.CalculatePayroll(taxStrategy));
            }
            return results;
        }

        public void AddDemoData()
        {
            if (_employees.Count > 0)
            {
                return;
            }

            Employee e1 = EmployeeFactory.CreateFullTimeEmployee("FT001", "Nguyễn Văn An", "Kế toán", 15000000m, 1500000m, 1000000m);
            e1.Attendance.WorkDays = 26;
            e1.Attendance.PaidLeaveDays = 1;
            e1.Attendance.UnpaidLeaveDays = 0;
            e1.Attendance.LateDays = 1;
            e1.Attendance.OvertimeHours = 8;

            Employee e2 = EmployeeFactory.CreateFullTimeEmployee("FT002", "Trần Thị Bình", "Nhân sự", 13000000m, 1200000m, 500000m);
            e2.Attendance.WorkDays = 24;
            e2.Attendance.PaidLeaveDays = 0;
            e2.Attendance.UnpaidLeaveDays = 2;
            e2.Attendance.LateDays = 3;
            e2.Attendance.OvertimeHours = 4;

            Employee e3 = EmployeeFactory.CreatePartTimeEmployee("PT001", "Lê Minh Cường", "Kho hàng", 55000m);
            e3.Attendance.WorkDays = 18;
            e3.Attendance.WorkHours = 96;
            e3.Attendance.LateDays = 2;
            e3.Attendance.OvertimeHours = 6;

            Employee e4 = EmployeeFactory.CreatePartTimeEmployee("PT002", "Phạm Gia Hân", "Bán hàng", 60000m);
            e4.Attendance.WorkDays = 20;
            e4.Attendance.WorkHours = 120;
            e4.Attendance.LateDays = 0;
            e4.Attendance.OvertimeHours = 10;

            AddEmployee(e1);
            AddEmployee(e2);
            AddEmployee(e3);
            AddEmployee(e4);
        }
    }
}
