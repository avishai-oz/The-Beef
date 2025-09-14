using The_Beef.Domain.Entities;

namespace The_Beef.Application.Ports;

public interface IUserRepository
{
    Task<User> Get(string userId);
    Task Save(User user);
}