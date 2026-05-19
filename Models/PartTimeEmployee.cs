using System;
using ChamCongTinhLuongOOP.Utilities;

namespace ChamCongTinhLuongOOP.Models
{
    public class PartTimeEmployee : Employee
    {
        public const decimal LatePenaltyPerDay = 30000m;
        public const decimal OvertimeMultiplier = 1.5m;

        public decimal HourlyRate { get; private set; }

        public PartTimeEmployee(string id, string fullName, string department, decimal hourlyRate)
            : base(id, fullName, department, 0)
        {
            if (hourlyRate < 0)
            {
                throw new ArgumentException("Lương theo giờ không được âm.");
            }
            HourlyRate = hourlyRate;
        }

        public override string GetEmployeeType()
        {
            return "Part-time";
        }

        public override decimal CalculateGrossSalary()
        {
            decimal normalMoney = Attendance.WorkHours * HourlyRate;
            decimal overtimeMoney = Attendance.OvertimeHours * HourlyRate * OvertimeMultiplier;
            decimal latePenalty = Attendance.LateDays * LatePenaltyPerDay;

            decimal gross = normalMoney + overtimeMoney - latePenalty;
            return Math.Round(gross, 0);
        }

        public override string GetSalaryFormulaNote()
        {
            return "Giờ làm * lương giờ + tăng ca 150% - phạt đi trễ";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Lương/giờ   : " + MoneyFormatter.Format(HourlyRate));
        }
    }
}
