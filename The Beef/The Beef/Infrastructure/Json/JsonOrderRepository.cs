using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Infrastructure.Json;

public sealed class JsonOrderRepository : IOrderRepository
{
    private readonly string _path;
    public JsonOrderRepository(string dataDir)
        => _path = Path.Combine(dataDir, "orders.json");

    public async Task Save(Order order)
    {
        // ממפים את ה-Order לדאטה שנשמר—בלי User אובייקט
        var rec = new OrderRecord(
            order.OrderId,
            order.UserId,          
            order.TableID,
            order.Items.ToList(),  
            order.Subtotal,
            order.FinalAmount,
            order.userName,
            order.UserMembership,
            DateTime.UtcNow
        );

        var list = await JsonUtills.LoadListAsync<OrderRecord>(_path);
        list.Add(rec);
        await JsonUtills.SaveListAsync(_path, list);
    }
}