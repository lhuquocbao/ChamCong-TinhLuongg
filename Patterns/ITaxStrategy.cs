namespace ChamCongTinhLuongOOP.Patterns
{
    public interface ITaxStrategy
    {
        string StrategyName { get; }
        decimal CalculateTax(decimal grossSalary);
    }
}
