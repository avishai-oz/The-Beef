using The_Beef.Domain.Orders;
using The_Beef.Domain.Entities;
namespace The_Beef.Domain.Entities;

public class Order 
{
    public string OrderId { get; }
    public User User { get; }
    public string TableID { get; }
    
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public decimal Subtotal { get; private set; }
    public decimal FinalAmount { get; private set; }

    public Order(string orderId, User user, string tableId, IEnumerable<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(orderId)) throw new ArgumentException("OrderId is required.", nameof(orderId));
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrWhiteSpace(tableId)) throw new ArgumentException("TableId is required.", nameof(tableId));
        if (items is null) throw new ArgumentNullException(nameof(items));
        
        OrderId = orderId;
        User = user;
        TableID = tableId;
        
        _items.AddRange(items);
        if (_items.Count == 0) throw new InvalidOperationException("Order must contain at least one item.");
        
        RecalculateSubtotal();
        FinalAmount = Subtotal; 
    }

    public void AddItem(OrderItem item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _items.Add(item);
        RecalculateSubtotal();
        if (FinalAmount > Subtotal) FinalAmount = Subtotal; // לשמור אינבריאנט
    }

    public void ApplyFinal(decimal finalAmount)
    {
        if (finalAmount < 0m) throw new ArgumentOutOfRangeException(nameof(finalAmount));
        if (finalAmount > Subtotal) throw new InvalidOperationException("FinalAmount cannot exceed Subtotal.");
        FinalAmount = finalAmount;
    }

    private void RecalculateSubtotal()
        => Subtotal = _items.Sum(i => i.LineTotal);
}