using The_Beef.Domain.Enums;
namespace The_Beef.Domain.Pricing;

public class PricingStrategyProvider : IPricingStrategyProvider
{
    private readonly IPricingStrategy _regular = new RegularPricing();
    private readonly IPricingStrategy _premium = new PremiumPricing();

    public IPricingStrategy GetStrategy(MembershipType membership)
        => membership == MembershipType.Premium ? _premium : _regular;
}