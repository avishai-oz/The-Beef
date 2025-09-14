using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Application.Services;

public class DishSelectionService
{
    private readonly IMenuRepository _menu;
    public DishSelectionService(IMenuRepository menu) => _menu = menu ?? throw new ArgumentNullException(nameof(menu));

    public async Task<List<(Dish dish, int qty)>> ByNames(IEnumerable<(string name, int qty)> selections)
    {
        if (selections is null) throw new ArgumentNullException(nameof(selections));
        
        var normalizedSelections = selections
            .GroupBy(s => s.name.Trim(), StringComparer.OrdinalIgnoreCase)
            .Select(g => (name: g.Key, qty: g.Sum(x => x.qty)))
            .ToList();
        
        var resultOrder = new List<(Dish dish, int qty)>(normalizedSelections.Count);
        
        foreach (var (name, qty) in normalizedSelections)
        {
            if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(selections), "Quantity must be >= 1.");
            var dish = await _menu.FindByName(name)
                       ?? throw new InvalidOperationException($"Dish not found: {name}");
            resultOrder.Add((dish, qty));
        }

        return resultOrder;
    }
}