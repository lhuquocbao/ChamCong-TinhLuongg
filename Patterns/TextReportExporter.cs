using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChamCongTinhLuongOOP.Models;
using ChamCongTinhLuongOOP.Utilities;

namespace ChamCongTinhLuongOOP.Patterns
{
    public class TextReportExporter : IReportExporter
    {
        public void ExportPayrollReport(List<PayrollResult> payrollResults, string filePath, string taxStrategyName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("============================================================");
            builder.AppendLine("          BẢNG LƯƠNG NHÂN VIÊN CUỐI THÁNG");
            builder.AppendLine("============================================================");
            builder.AppendLine("Ngày xuất      : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            builder.AppendLine("Cách tính thuế : " + taxStrategyName);
            builder.AppendLine("Số nhân viên   : " + payrollResults.Count);
            builder.AppendLine("------------------------------------------------------------");

            decimal totalGross = 0;
            decimal totalTax = 0;
            decimal totalNet = 0;

            foreach (PayrollResult result in payrollResults)
            {
                builder.AppendLine(result.ToReportLine());
                builder.AppendLine("  Chấm công: " + result.Attendance.GetSummary());
                builder.AppendLine("------------------------------------------------------------");
                totalGross += result.GrossSalary;
                totalTax += result.Tax;
                totalNet += result.NetSalary;
            }

            builder.AppendLine("TỔNG LƯƠNG GROSS : " + MoneyFormatter.Format(totalGross));
            builder.AppendLine("TỔNG THUẾ        : " + MoneyFormatter.Format(totalTax));
            builder.AppendLine("TỔNG THỰC NHẬN   : " + MoneyFormatter.Format(totalNet));
            builder.AppendLine("============================================================");

            File.WriteAllText(filePath, builder.ToString(), Encoding.UTF8);
        }
    }
}
