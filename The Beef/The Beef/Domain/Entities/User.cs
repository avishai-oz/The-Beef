using The_Beef.Domain.Enums;

namespace The_Beef.Domain.Entities;

public class User
{
    public string Id { get;}
    public string Name { get; }
    public decimal Wallet { get; private set; }
    public MembershipType Membership { get; }
    
    public User(string id, string name, decimal wallet, MembershipType membership)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name needed", nameof(name));
        if(string.IsNullOrEmpty(id))
            throw new ArgumentException("Id needed", nameof(id));
        if (wallet < 0)
            throw new ArgumentOutOfRangeException(nameof(wallet), "Wallet cannot be negative.");
        
        Id = id.Trim();
        Name = name.Trim();
        Wallet = wallet;
        Membership = membership;
    }
     
    public void Debit (decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        if (amount > Wallet)
            throw new InvalidOperationException("Insufficient funds in wallet.");
        
        Wallet -= amount;
    }
    
    public void Credit(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        
        Wallet += amount;
    }
}