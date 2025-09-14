using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Infrastructure.Json;

public class JsonUserRepository :IUserRepository
{
    private readonly string _path;
    public JsonUserRepository(string dataDir)
        => _path = Path.Combine(dataDir, "users.json");

    public async Task<User?> Get(string userId)
    {
        var users = await JsonUtills.LoadListAsync<User>(_path);
        return users.FirstOrDefault(u => u.Id == userId);
    }

    public async Task Save(User user)
    {
        var users = await JsonUtills.LoadListAsync<User>(_path);
        var i = users.FindIndex(u => u.Id == user.Id);
        if (i >= 0) users[i] = user;
        else users.Add(user);
        await JsonUtills.SaveListAsync(_path, users);
    }
}