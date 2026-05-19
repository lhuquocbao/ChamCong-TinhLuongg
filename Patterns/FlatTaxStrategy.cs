using System;

namespace ChamCongTinhLuongOOP.Patterns
{
    public class FlatTaxStrategy : ITaxStrategy
    {
        public string StrategyName
        {
            get { return "Thuế cố định 10%"; }
        }

        public decimal CalculateTax(decimal grossSalary)
        {
            if (grossSalary <= 0)
            {
                return 0;
            }
            return Math.Round(grossSalary * 0.10m, 0);
        }
    }
}
