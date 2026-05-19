using System.Collections.Generic;
using ChamCongTinhLuongOOP.Models;

namespace ChamCongTinhLuongOOP.Patterns
{
    public interface IReportExporter
    {
        void ExportPayrollReport(List<PayrollResult> payrollResults, string filePath, string taxStrategyName);
    }
}
