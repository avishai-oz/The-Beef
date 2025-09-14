namespace The_Beef.Domain.Pricing;

public class RegularPricing : IPricingStrategy
{
    public decimal CalculateTotal(IEnumerable<decimal> lineTotals)
    {
        if (lineTotals is null) throw new ArgumentNullException(nameof(lineTotals));
            return lineTotals.Sum();
    }
}