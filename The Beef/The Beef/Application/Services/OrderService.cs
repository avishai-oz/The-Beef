using The_Beef.Domain.Pricing;     
using The_Beef.Domain.Entities;        
using The_Beef.Domain.Orders;
using The_Beef.Domain.Enums;

namespace The_Beef.Application.Services;

/*public class OrderService
{
    private readonly IPricingStrategyProvider _pricing;
    private readonly IMenuRepository _menu;
    private readonly ITableRepository _tables;
    private readonly IUserRepository _users;
    private readonly IOrderRepository _orders;+
    private readonly IReceiptRenderer _renderer;

    public OrderService(
        IPricingStrategyProvider pricing,
        IMenuRepository menu,
        ITableRepository tables,
        IUserRepository users,
        IOrderRepository orders,
        IReceiptRenderer renderer)
    {
        _pricing = pricing ?? throw new ArgumentNullException(nameof(pricing));
        _menu = menu; _tables = tables; _users = users; _orders = orders; _renderer = renderer;
    }


    private decimal CalculateFinalAmount(MembershipType membership, IReadOnlyList<OrderItem> items)
    {
        var lineTotals = items.Select(i => i.LineTotal);
        var strategy = _pricing.GetStrategy(membership);           // בוחר אסטרטגיה לפי חברות
        var gross = strategy.CalculateTotal(lineTotals);   // מחשב סכום עם/בלי הנחה
        return Math.Round(gross, 2, MidpointRounding.AwayFromZero); // עיגול סופי
    }
}
