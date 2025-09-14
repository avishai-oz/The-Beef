using The_Beef.Domain.Orders;
namespace The_Beef.Domain.Entities;

public class Order
{
    public string OrderId { get; }
    public User User { get; }
}