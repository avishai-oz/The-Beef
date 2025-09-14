using The_Beef.Domain.Entities;
namespace The_Beef.Application.Ports;

public interface ITableRepository
{
    Task<Table> GetAvailable(int requiredSeats);
    Task Save(Table table);
}