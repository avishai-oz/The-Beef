namespace The_Beef.Domain.Pricing;

public interface IPricingStrategy
{
    decimal CalculateTotal(IEnumerable<decimal> lineTotals);
}