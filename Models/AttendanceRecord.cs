using System;

namespace ChamCongTinhLuongOOP.Models
{
    public class AttendanceRecord
    {
        public int WorkDays { get; set; }
        public decimal WorkHours { get; set; }
        public int PaidLeaveDays { get; set; }
        public int UnpaidLeaveDays { get; set; }
        public int LateDays { get; set; }
        public decimal OvertimeHours { get; set; }

        public AttendanceRecord()
        {
            WorkDays = 0;
            WorkHours = 0;
            PaidLeaveDays = 0;
            UnpaidLeaveDays = 0;
            LateDays = 0;
            OvertimeHours = 0;
        }

        public string GetSummary()
        {
            return string.Format(
                "Ngày công: {0}, Giờ làm: {1}, Nghỉ phép: {2}, Nghỉ không phép: {3}, Đi trễ: {4}, Tăng ca: {5}",
                WorkDays,
                WorkHours,
                PaidLeaveDays,
                UnpaidLeaveDays,
                LateDays,
                OvertimeHours
            );
        }
    }
}
