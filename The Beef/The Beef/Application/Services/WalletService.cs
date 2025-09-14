using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Application.Services;

public class WalletService
{
    private readonly IUserRepository _user;
    public WalletService(IUserRepository user) => _user = user ?? throw new ArgumentNullException(nameof(user));
    
    public bool HasBalance(User user, decimal amount) => user.Wallet >= amount;

    public async Task Debit(User user, decimal amount, CancellationToken ct = default)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (amount < 0m)  throw new ArgumentOutOfRangeException(nameof(amount));
        if (!HasBalance(user, amount)) throw new InvalidOperationException("Insufficient funds.");

        user.Debit(amount);
        await _user.Save(user);
    }
}