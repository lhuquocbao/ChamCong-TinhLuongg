using System.Collections.Generic;
using System.Linq;
using ChamCongTinhLuongOOP.Models;

namespace ChamCongTinhLuongOOP.Services
{
    public class StatisticsService
    {
        public decimal GetTotalGrossSalary(List<PayrollResult> results)
        {
            return results.Sum(r => r.GrossSalary);
        }

        public decimal GetTotalTax(List<PayrollResult> results)
        {
            return results.Sum(r => r.Tax);
        }

        public decimal GetTotalNetSalary(List<PayrollResult> results)
        {
            return results.Sum(r => r.NetSalary);
        }

        public PayrollResult GetHighestNetSalaryEmployee(List<PayrollResult> results)
        {
            return results.OrderByDescending(r => r.NetSalary).FirstOrDefault();
        }

        public PayrollResult GetMostLateEmployee(List<PayrollResult> results)
        {
            return results.OrderByDescending(r => r.Attendance.LateDays).FirstOrDefault();
        }
    }
}
