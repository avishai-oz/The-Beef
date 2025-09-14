using The_Beef.Domain.Enums;
using The_Beef.Domain.Orders;

namespace The_Beef.Infrastructure.Json;

public sealed record OrderRecord(
    string OrderId,
    string UserId,
    string TableId,
    List<OrderItem> Items,
    decimal Subtotal,
    decimal FinalAmount,
    string UserNameAtPurchase,
    MembershipType MembershipAtPurchase,
    DateTime CreatedAtUtc
);