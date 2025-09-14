using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Application.Services;

public class SeatingService
{
    private readonly ITableRepository _tables;
    
    public SeatingService(ITableRepository tables)
        => _tables = tables ?? throw new ArgumentNullException(nameof(tables));

    public async Task<Table> AssignTable(User user, int partySize)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (partySize <= 0) throw new ArgumentOutOfRangeException(nameof(partySize));

        var table = await _tables.GetAvailable(partySize)
                    ?? throw new InvalidOperationException("No table available for requested size.");


        table.MarkAsOccupied();
        await _tables.Save(table);

        return table;
    }
        
    
}