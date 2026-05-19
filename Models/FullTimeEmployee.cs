using System;
using ChamCongTinhLuongOOP.Utilities;

namespace ChamCongTinhLuongOOP.Models
{
    public class FullTimeEmployee : Employee
    {
        public const int StandardWorkingDays = 26;
        public const decimal LatePenaltyPerDay = 50000m;
        public const decimal OvertimeRatePerHour = 70000m;

        public decimal Allowance { get; set; }
        public decimal Bonus { get; set; }

        public FullTimeEmployee(string id, string fullName, string department,
            decimal baseSalary, decimal allowance, decimal bonus)
            : base(id, fullName, department, baseSalary)
        {
            Allowance = allowance < 0 ? 0 : allowance;
            Bonus = bonus < 0 ? 0 : bonus;
        }

        public override string GetEmployeeType()
        {
            return "Full-time";
        }

        public override decimal CalculateGrossSalary()
        {
            decimal overtimeMoney = Attendance.OvertimeHours * OvertimeRatePerHour;
            decimal latePenalty = Attendance.LateDays * LatePenaltyPerDay;
            decimal unpaidLeavePenalty = 0;

            if (StandardWorkingDays > 0)
            {
                unpaidLeavePenalty = (BaseSalary / StandardWorkingDays) * Attendance.UnpaidLeaveDays;
            }

            decimal gross = BaseSalary + Allowance + Bonus + overtimeMoney - latePenalty - unpaidLeavePenalty;
            return Math.Round(gross, 0);
        }

        public override string GetSalaryFormulaNote()
        {
            return "Lương cơ bản + phụ cấp + thưởng + tăng ca - phạt đi trễ - nghỉ không phép";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Phụ cấp     : " + MoneyFormatter.Format(Allowance));
            Console.WriteLine("Thưởng      : " + MoneyFormatter.Format(Bonus));
        }
    }
}
