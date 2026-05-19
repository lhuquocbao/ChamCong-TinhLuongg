using System.Globalization;

namespace ChamCongTinhLuongOOP.Utilities
{
    public static class MoneyFormatter
    {
        public static string Format(decimal amount)
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            return amount.ToString("N0", culture) + " VND";
        }
    }
}
