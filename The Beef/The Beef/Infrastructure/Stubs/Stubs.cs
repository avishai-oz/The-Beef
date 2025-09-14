using System.Collections.Concurrent;
using The_Beef.Application.Ports;
using The_Beef.Application.DOTs;
using The_Beef.Domain.Entities;

namespace The_Beef.Infrastructure.Stubs;

// ===== Menu =====
public sealed class StubMenuRepository : IMenuRepository
{
    private readonly ConcurrentDictionary<string, Dish> _byId = new(StringComparer.Ordinal);
    private readonly ConcurrentDictionary<string, Dish> _byName = new(StringComparer.OrdinalIgnoreCase);

    public StubMenuRepository(IEnumerable<Dish>? seed = null)
    {
        var items = seed ?? new[]
        {
            new Dish("D1","Burger", 42.00m),
            new Dish("D2","Fries",  15.50m),
            new Dish("D3","Cola",    8.00m),
        };
        foreach (var d in items) AddOrReplace(d);
    }
    private void AddOrReplace(Dish d) { _byId[d.Id]=d; _byName[d.Name]=d; }

    public Task<Dish?> FindByName(string name)
    {
        _byName.TryGetValue(name, out var d); 
        return Task.FromResult(d);
    }

    public Task<IReadOnlyList<Dish>> FindByIds(IEnumerable<string> ids)
    {
        var list = new List<Dish>();
        foreach (var id in ids) if (_byId.TryGetValue(id, out var d)) list.Add(d);
        return Task.FromResult((IReadOnlyList<Dish>)list);
    }
}

// ===== Tables =====
public sealed class StubTableRepository : ITableRepository
{
    private readonly List<Table> _tables = new()
    {
        new Table("T1", 2,  true),
        new Table("T2", 4,  true),
        new Table("T3", 6,  true),
    };

    public Task<Table?> GetAvailable(int requiredSeats)
        => Task.FromResult(_tables.FirstOrDefault(t => t.IsAvailable && t.Seats >= requiredSeats));

    public Task Save(Table table) => Task.CompletedTask;
}

// ===== Users =====
public sealed class StubUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> _users = new(StringComparer.Ordinal)
    {
        // שנה כאן אם תרצה משתמש רגיל
        ["U1"] = new User("U1", "Dana", wallet: 120.00m, membership: The_Beef.Domain.Enums.MembershipType.Premium)
    };

    public Task<User?> Get(string userId)
        => Task.FromResult(_users.TryGetValue(userId, out var u) ? u : null);

    public Task Save(User user)
    { _users[user.Id] = user; return Task.CompletedTask; }
}

// ===== Orders =====
public sealed class StubOrderRepository : IOrderRepository
{
    public readonly List<Order> Saved = new();
    public Task Save(Order order)
    { Saved.Add(order); return Task.CompletedTask; }
}

// ===== Renderer =====
public sealed class ConsoleReceiptRenderer : IReceiptRenderer
{
    public void Render(ReceiptDto dto)
    {
        var c = System.Globalization.CultureInfo.InvariantCulture;
        Console.WriteLine("===== RECEIPT =====");
        Console.WriteLine($"Order:   {dto.OrderId}");
        Console.WriteLine($"User:    {dto.UserName}");
        Console.WriteLine($"Table:   {dto.TableId}");
        Console.WriteLine($"When:    {dto.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC");
        Console.WriteLine("-------------------");
        foreach (var it in dto.Items)
            Console.WriteLine($"{it.DishName} x{it.Quantity} @ {it.UnitPrice.ToString("0.00",c)} = {it.LineTotal.ToString("0.00",c)}");
        Console.WriteLine("-------------------");
        Console.WriteLine($"Subtotal: {dto.Subtotal.ToString("0.00", c)}");
        Console.WriteLine($"Discount: {dto.DiscountAmount.ToString("0.00", c)}");
        Console.WriteLine($"TOTAL:    {dto.FinalAmount.ToString("0.00", c)}");
        Console.WriteLine($"Wallet: Befoul: {dto.WalletBefore.ToString("0.00", c)} → After: {dto.WalletAfter.ToString("0.00", c)}");
        Console.WriteLine("===================");
    }
}
