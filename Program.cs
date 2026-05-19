using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChamCongTinhLuongOOP.Models;
using ChamCongTinhLuongOOP.Patterns;
using ChamCongTinhLuongOOP.Services;
using ChamCongTinhLuongOOP.Utilities;

namespace ChamCongTinhLuongOOP
{
    class Program
    {
        private static readonly PayrollManager Manager = PayrollManager.Instance;
        private static readonly ITaxStrategy TaxStrategy = new ProgressiveTaxStrategy();
        private static readonly IReportExporter ReportExporter = new TextReportExporter();
        private static readonly StatisticsService Statistics = new StatisticsService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                ShowMenu();
                int choice = InputHelper.ReadInt("Chọn chức năng: ", 0, 11);
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddFullTimeEmployee();
                            break;
                        case 2:
                            AddPartTimeEmployee();
                            break;
                        case 3:
                            DisplayEmployees();
                            break;
                        case 4:
                            UpdateAttendance();
                            break;
                        case 5:
                            CalculateOneEmployee();
                            break;
                        case 6:
                            CalculateAllEmployees();
                            break;
                        case 7:
                            ShowStatistics();
                            break;
                        case 8:
                            ExportPayrollReport();
                            break;
                        case 9:
                            SearchEmployee();
                            break;
                        case 10:
                            RemoveEmployee();
                            break;
                        case 11:
                            AddDemoData();
                            break;
                        case 0:
                            running = false;
                            Console.WriteLine("Đã thoát chương trình.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }

                if (running)
                {
                    InputHelper.Pause();
                }
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("     HỆ THỐNG CHẤM CÔNG & TÍNH LƯƠNG NHÂN VIÊN");
            Console.WriteLine("============================================================");
            Console.WriteLine("OOP: Abstract Class, Kế thừa, Đa hình, Đóng gói");
            Console.WriteLine("Pattern: Factory Method, Strategy, Singleton");
            Console.WriteLine("Cách tính thuế hiện tại: " + TaxStrategy.StrategyName);
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("1.  Thêm nhân viên Full-time");
            Console.WriteLine("2.  Thêm nhân viên Part-time");
            Console.WriteLine("3.  Hiển thị danh sách nhân viên");
            Console.WriteLine("4.  Cập nhật chấm công");
            Console.WriteLine("5.  Tính lương một nhân viên");
            Console.WriteLine("6.  Tính lương toàn bộ nhân viên");
            Console.WriteLine("7.  Xem thống kê bảng lương");
            Console.WriteLine("8.  Xuất bảng lương ra file TXT");
            Console.WriteLine("9.  Tìm kiếm nhân viên");
            Console.WriteLine("10. Xóa nhân viên");
            Console.WriteLine("11. Tạo dữ liệu mẫu để demo");
            Console.WriteLine("0.  Thoát");
            Console.WriteLine("============================================================");
        }

        private static void AddFullTimeEmployee()
        {
            Console.WriteLine("THÊM NHÂN VIÊN FULL-TIME");
            string id = InputHelper.ReadString("Mã nhân viên: ");
            string name = InputHelper.ReadString("Họ tên: ");
            string department = InputHelper.ReadString("Phòng ban: ");
            decimal baseSalary = InputHelper.ReadDecimal("Lương cơ bản: ", 0, 1000000000);
            decimal allowance = InputHelper.ReadDecimal("Phụ cấp: ", 0, 1000000000);
            decimal bonus = InputHelper.ReadDecimal("Thưởng: ", 0, 1000000000);

            Employee employee = EmployeeFactory.CreateFullTimeEmployee(id, name, department, baseSalary, allowance, bonus);
            Manager.AddEmployee(employee);
            Console.WriteLine("Đã thêm nhân viên Full-time thành công.");
        }

        private static void AddPartTimeEmployee()
        {
            Console.WriteLine("THÊM NHÂN VIÊN PART-TIME");
            string id = InputHelper.ReadString("Mã nhân viên: ");
            string name = InputHelper.ReadString("Họ tên: ");
            string department = InputHelper.ReadString("Phòng ban: ");
            decimal hourlyRate = InputHelper.ReadDecimal("Lương theo giờ: ", 0, 10000000);

            Employee employee = EmployeeFactory.CreatePartTimeEmployee(id, name, department, hourlyRate);
            Manager.AddEmployee(employee);
            Console.WriteLine("Đã thêm nhân viên Part-time thành công.");
        }

        private static void DisplayEmployees()
        {
            List<Employee> employees = Manager.GetAllEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("Chưa có nhân viên nào.");
                return;
            }

            Console.WriteLine("DANH SÁCH NHÂN VIÊN");
            foreach (Employee employee in employees)
            {
                employee.DisplayInfo();
            }
        }

        private static void UpdateAttendance()
        {
            Console.WriteLine("CẬP NHẬT CHẤM CÔNG");
            Employee employee = FindEmployeeOrNotify();
            if (employee == null)
            {
                return;
            }

            Console.WriteLine("Đang cập nhật cho: " + employee.FullName + " - " + employee.GetEmployeeType());
            employee.Attendance.WorkDays = InputHelper.ReadInt("Số ngày công: ", 0, 31);
            employee.Attendance.WorkHours = InputHelper.ReadDecimal("Số giờ làm: ", 0, 500);
            employee.Attendance.PaidLeaveDays = InputHelper.ReadInt("Số ngày nghỉ phép: ", 0, 31);
            employee.Attendance.UnpaidLeaveDays = InputHelper.ReadInt("Số ngày nghỉ không phép: ", 0, 31);
            employee.Attendance.LateDays = InputHelper.ReadInt("Số lần/ngày đi trễ: ", 0, 31);
            employee.Attendance.OvertimeHours = InputHelper.ReadDecimal("Số giờ tăng ca: ", 0, 300);

            Console.WriteLine("Đã cập nhật chấm công thành công.");
        }

        private static void CalculateOneEmployee()
        {
            Console.WriteLine("TÍNH LƯƠNG MỘT NHÂN VIÊN");
            Employee employee = FindEmployeeOrNotify();
            if (employee == null)
            {
                return;
            }

            PayrollResult result = employee.CalculatePayroll(TaxStrategy);
            PrintPayrollHeader();
            PrintPayrollRow(result);
        }

        private static void CalculateAllEmployees()
        {
            Console.WriteLine("TÍNH LƯƠNG TOÀN BỘ NHÂN VIÊN");
            List<PayrollResult> results = Manager.CalculateAllPayrolls(TaxStrategy);
            if (results.Count == 0)
            {
                Console.WriteLine("Chưa có nhân viên nào.");
                return;
            }

            PrintPayrollHeader();
            foreach (PayrollResult result in results)
            {
                PrintPayrollRow(result);
            }
        }

        private static void ShowStatistics()
        {
            Console.WriteLine("THỐNG KÊ BẢNG LƯƠNG");
            List<PayrollResult> results = Manager.CalculateAllPayrolls(TaxStrategy);
            if (results.Count == 0)
            {
                Console.WriteLine("Chưa có dữ liệu để thống kê.");
                return;
            }

            PayrollResult highestNet = Statistics.GetHighestNetSalaryEmployee(results);
            PayrollResult mostLate = Statistics.GetMostLateEmployee(results);

            Console.WriteLine("Tổng số nhân viên : " + results.Count);
            Console.WriteLine("Tổng lương gross  : " + MoneyFormatter.Format(Statistics.GetTotalGrossSalary(results)));
            Console.WriteLine("Tổng thuế         : " + MoneyFormatter.Format(Statistics.GetTotalTax(results)));
            Console.WriteLine("Tổng thực nhận    : " + MoneyFormatter.Format(Statistics.GetTotalNetSalary(results)));
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("NV lương cao nhất : {0} - {1}", highestNet.FullName, MoneyFormatter.Format(highestNet.NetSalary));
            Console.WriteLine("NV đi trễ nhiều   : {0} - {1} lần/ngày", mostLate.FullName, mostLate.Attendance.LateDays);
        }

        private static void ExportPayrollReport()
        {
            Console.WriteLine("XUẤT BẢNG LƯƠNG RA FILE TXT");
            List<PayrollResult> results = Manager.CalculateAllPayrolls(TaxStrategy);
            if (results.Count == 0)
            {
                Console.WriteLine("Chưa có nhân viên để xuất file.");
                return;
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PayrollReport.txt");
            ReportExporter.ExportPayrollReport(results, filePath, TaxStrategy.StrategyName);
            Console.WriteLine("Đã xuất bảng lương thành công.");
            Console.WriteLine("Đường dẫn file: " + filePath);
        }

        private static void SearchEmployee()
        {
            Console.WriteLine("TÌM KIẾM NHÂN VIÊN");
            Console.WriteLine("1. Tìm theo mã");
            Console.WriteLine("2. Tìm theo tên");
            int choice = InputHelper.ReadInt("Chọn kiểu tìm kiếm: ", 1, 2);

            if (choice == 1)
            {
                Employee employee = FindEmployeeOrNotify();
                if (employee != null)
                {
                    employee.DisplayInfo();
                }
            }
            else
            {
                string keyword = InputHelper.ReadString("Nhập tên cần tìm: ");
                List<Employee> results = Manager.SearchByName(keyword);
                if (results.Count == 0)
                {
                    Console.WriteLine("Không tìm thấy nhân viên phù hợp.");
                    return;
                }

                foreach (Employee employee in results)
                {
                    employee.DisplayInfo();
                }
            }
        }

        private static void RemoveEmployee()
        {
            Console.WriteLine("XÓA NHÂN VIÊN");
            string id = InputHelper.ReadString("Nhập mã nhân viên cần xóa: ");
            bool success = Manager.RemoveEmployee(id);
            Console.WriteLine(success ? "Đã xóa nhân viên." : "Không tìm thấy nhân viên.");
        }

        private static void AddDemoData()
        {
            if (Manager.GetAllEmployees().Count > 0)
            {
                Console.WriteLine("Đã có dữ liệu. Chức năng demo chỉ thêm khi danh sách đang rỗng.");
                return;
            }

            Manager.AddDemoData();
            Console.WriteLine("Đã tạo 4 nhân viên mẫu để demo.");
        }

        private static Employee FindEmployeeOrNotify()
        {
            string id = InputHelper.ReadString("Nhập mã nhân viên: ");
            Employee employee = Manager.FindEmployeeById(id);
            if (employee == null)
            {
                Console.WriteLine("Không tìm thấy nhân viên có mã: " + id);
            }
            return employee;
        }

        private static void PrintPayrollHeader()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-10} | {1,-25} | {2,-10} | {3,18} | {4,18} | {5,18}",
                "Mã NV", "Họ tên", "Loại", "Gross", "Thuế", "Thực nhận");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        }

        private static void PrintPayrollRow(PayrollResult result)
        {
            Console.WriteLine("{0,-10} | {1,-25} | {2,-10} | {3,18} | {4,18} | {5,18}",
                result.EmployeeId,
                result.FullName,
                result.EmployeeType,
                MoneyFormatter.Format(result.GrossSalary),
                MoneyFormatter.Format(result.Tax),
                MoneyFormatter.Format(result.NetSalary));
        }
    }
}
