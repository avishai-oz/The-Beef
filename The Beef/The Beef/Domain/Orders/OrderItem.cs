namespace The_Beef.Domain.Orders;

public class OrderItem
{
    public string DishID { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    
    public decimal LineTotal => Quantity * UnitPrice;

    public OrderItem(string dishID, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(dishID))
            throw new ArgumentException("DishID needed", nameof(dishID));
        if (quantity < 1)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (unitPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice cannot be negative.");
        
        DishID = dishID;    
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
