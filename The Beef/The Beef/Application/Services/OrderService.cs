using The_Beef.Domain.Entities;      
using The_Beef.Domain.Orders;  
using The_Beef.Domain.Enums;       

namespace The_Beef.Application.Services;

public sealed class OrderService
{
    public Order Create(User user, Table table, List<(Dish dish, int qty)> dishSelection)
    {
        if (user is null)  throw new ArgumentNullException(nameof(user));
        if (table is null) throw new ArgumentNullException(nameof(table));
        if (dishSelection is null || dishSelection.Count == 0) throw new ArgumentException("No picks.", nameof(dishSelection));
        
        var items = dishSelection.Select(p =>
        {
            if (p.qty <= 0) throw new ArgumentOutOfRangeException(nameof(dishSelection), "Quantity must be >= 1.");
            return new OrderItem(p.dish.Id, p.qty, p.dish.Price);
        }).ToList();
        
        var order = new Order(Guid.NewGuid().ToString("N"), user, table.Id, items);
        
        var finalAmount = CalculateFinalAmount(user.Membership, items);
        order.ApplyFinal(finalAmount);

        return order;
    }
    
    public decimal CalculateFinalAmount(MembershipType membership, IReadOnlyList<OrderItem> items)
    {
        if (items is null || items.Count == 0) throw new ArgumentException("Items required.", nameof(items));

        var subtotal = items.Sum(i => i.LineTotal);
        var gross = membership == MembershipType.Premium ? subtotal * 0.90m : subtotal;
        return Math.Round(gross, 2, MidpointRounding.AwayFromZero); 
    }
}