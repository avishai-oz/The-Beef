
using The_Beef.Domain.Entities;

namespace The_Beef.Application.Ports;

public interface IMenuRepository
{
    Task<Dish> FindByName(string name);
    Task<IReadOnlyList<Dish>> FindByIds(IEnumerable<string> ids);
}