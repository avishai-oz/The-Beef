using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Infrastructure.Json;

public class JsonTableRepository : ITableRepository
{
    private readonly string _path;
    public JsonTableRepository(string dataDir)
        => _path = Path.Combine(dataDir, "tables.json");

    public async Task<Table?> GetAvailable(int requiredSeats)
    {
        var tables = await JsonUtills.LoadListAsync<Table>(_path);
        return tables.FirstOrDefault(t => t.IsAvailable && t.Seats >= requiredSeats);
    }

    public async Task Save(Table table)
    {
        var tables = await JsonUtills.LoadListAsync<Table>(_path);
        var i = tables.FindIndex(t => t.Id == table.Id);
        if (i >= 0) tables[i] = table;
        else tables.Add(table);
        await JsonUtills.SaveListAsync(_path, tables);
    }
}