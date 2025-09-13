using The_Beef.Domain.Enums;

namespace The_Beef.Domain.Entities;

public class User
{
    public int Id { get;}
    public string Name { get; }
    public decimal Wallet { get; private set; }
    public MembershipType Membership { get; }
}