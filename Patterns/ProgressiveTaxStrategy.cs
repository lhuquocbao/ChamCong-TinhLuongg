using System;

namespace ChamCongTinhLuongOOP.Patterns
{
    public class ProgressiveTaxStrategy : ITaxStrategy
    {
        public string StrategyName
        {
            get { return "Thuế lũy tiến mô phỏng"; }
        }

        public decimal CalculateTax(decimal grossSalary)
        {
            // Mô phỏng thuế TNCN lũy tiến để phục vụ bài OOP.
            // Giảm trừ cá nhân giả lập: 11.000.000 VND.
            decimal taxableIncome = grossSalary - 11000000m;
            if (taxableIncome <= 0)
            {
                return 0;
            }

            decimal tax = 0;
            decimal remaining = taxableIncome;

            tax += CalculateBracketTax(ref remaining, 5000000m, 0.05m);
            tax += CalculateBracketTax(ref remaining, 5000000m, 0.10m);
            tax += CalculateBracketTax(ref remaining, 8000000m, 0.15m);
            tax += CalculateBracketTax(ref remaining, 14000000m, 0.20m);

            if (remaining > 0)
            {
                tax += remaining * 0.25m;
            }

            return Math.Round(tax, 0);
        }

        private decimal CalculateBracketTax(ref decimal remaining, decimal bracketSize, decimal rate)
        {
            if (remaining <= 0)
            {
                return 0;
            }

            decimal amountInBracket = Math.Min(remaining, bracketSize);
            remaining -= amountInBracket;
            return amountInBracket * rate;
        }
    }
}
