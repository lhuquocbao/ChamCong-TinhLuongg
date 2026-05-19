using System;
using System.Globalization;

namespace ChamCongTinhLuongOOP.Utilities
{
    public static class InputHelper
    {
        public static string ReadString(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("Dữ liệu không được rỗng. Vui lòng nhập lại.");
            }
        }

        public static int ReadInt(string label, int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();
                int value;
                if (int.TryParse(input, out value) && value >= minValue && value <= maxValue)
                {
                    return value;
                }
                Console.WriteLine("Vui lòng nhập số nguyên từ {0} đến {1}.", minValue, maxValue);
            }
        }

        public static decimal ReadDecimal(string label, decimal minValue, decimal maxValue)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Replace(",", ".");
                }

                decimal value;
                if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out value)
                    && value >= minValue && value <= maxValue)
                {
                    return value;
                }
                Console.WriteLine("Vui lòng nhập số từ {0} đến {1}.", minValue, maxValue);
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Nhấn Enter để tiếp tục...");
            Console.ReadLine();
        }
    }
}
