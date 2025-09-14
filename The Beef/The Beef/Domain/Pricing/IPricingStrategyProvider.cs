using The_Beef.Domain.Enums;
namespace The_Beef.Domain.Pricing;

public interface IPricingStrategyProvider
{
    IPricingStrategy GetStrategy(MembershipType membership);

}