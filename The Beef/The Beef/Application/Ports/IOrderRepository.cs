using The_Beef.Domain.Entities;

namespace The_Beef.Application.Ports;

public interface IOrderRepository
{
    Task Save(Order order);
    
}