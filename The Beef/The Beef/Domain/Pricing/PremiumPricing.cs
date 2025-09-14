namespace The_Beef.Domain.Pricing;

public class PremiumPricing : IPricingStrategy
{
    private const decimal DiscountFactor = 0.90m;

    public decimal CalculateTotal(IEnumerable<decimal> lineTotals)
    {
        if (lineTotals is null) throw new ArgumentNullException(nameof(lineTotals));
        var subtotal = lineTotals.Sum();
        return subtotal * DiscountFactor;
    }
}