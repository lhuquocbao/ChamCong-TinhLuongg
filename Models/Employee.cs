using System;
using ChamCongTinhLuongOOP.Patterns;
using ChamCongTinhLuongOOP.Utilities;

namespace ChamCongTinhLuongOOP.Models
{
    public abstract class Employee
    {
        private string _id;
        private string _fullName;
        private string _department;
        private decimal _baseSalary;

        public string Id
        {
            get { return _id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Mã nhân viên không được rỗng.");
                }
                _id = value.Trim().ToUpper();
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Họ tên không được rỗng.");
                }
                _fullName = value.Trim();
            }
        }

        public string Department
        {
            get { return _department; }
            set { _department = string.IsNullOrWhiteSpace(value) ? "Chưa phân phòng" : value.Trim(); }
        }

        public decimal BaseSalary
        {
            get { return _baseSalary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Lương không được âm.");
                }
                _baseSalary = value;
            }
        }

        public AttendanceRecord Attendance { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Employee(string id, string fullName, string department, decimal baseSalary)
        {
            Id = id;
            FullName = fullName;
            Department = department;
            BaseSalary = baseSalary;
            Attendance = new AttendanceRecord();
            CreatedAt = DateTime.Now;
        }

        public abstract string GetEmployeeType();
        public abstract decimal CalculateGrossSalary();
        public abstract string GetSalaryFormulaNote();

        public PayrollResult CalculatePayroll(ITaxStrategy taxStrategy)
        {
            decimal gross = CalculateGrossSalary();
            if (gross < 0)
            {
                gross = 0;
            }

            decimal tax = taxStrategy.CalculateTax(gross);
            if (tax < 0)
            {
                tax = 0;
            }

            decimal net = gross - tax;
            return new PayrollResult(Id, FullName, GetEmployeeType(), gross, tax, net, Attendance);
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Mã NV       : " + Id);
            Console.WriteLine("Họ tên      : " + FullName);
            Console.WriteLine("Phòng ban   : " + Department);
            Console.WriteLine("Loại NV     : " + GetEmployeeType());
            Console.WriteLine("Lương gốc   : " + MoneyFormatter.Format(BaseSalary));
            Console.WriteLine("Chấm công   : " + Attendance.GetSummary());
            Console.WriteLine("Công thức   : " + GetSalaryFormulaNote());
        }
    }
}
