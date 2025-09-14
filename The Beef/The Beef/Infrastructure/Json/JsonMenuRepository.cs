using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;
using The_Beef.Infrastructure.Json;

namespace The_Beef.Infrastruure.Json;

public class JsonMenuRepository : IMenuRepository
{
    private readonly string _path;
    private List<Dish>? _cache;
    private DateTime _loadedAt;
    private static readonly TimeSpan Ttl = TimeSpan.FromSeconds(30);

    public JsonMenuRepository(string dataDir)
        => _path = Path.Combine(dataDir, "menu.json");

    private async Task<List<Dish>> LoadAsync()
    {
        if (_cache is null || DateTime.UtcNow - _loadedAt > Ttl)
        {
            _cache = await JsonUtills.LoadListAsync<Dish>(_path);
            _loadedAt = DateTime.UtcNow;
        }
        return _cache;
    }

    public async Task<Dish?> FindByName(string name)
    {
        var all = await LoadAsync();
        return all.FirstOrDefault(d => string.Equals(d.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IReadOnlyList<Dish>> FindByIds(IEnumerable<string> ids)
    {
        var all = await LoadAsync();
        var set = ids.ToHashSet(StringComparer.Ordinal);
        return all.Where(d => set.Contains(d.Id)).ToList();
    }
}