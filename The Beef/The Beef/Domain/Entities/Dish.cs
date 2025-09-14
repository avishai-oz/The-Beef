namespace The_Beef.Domain.Entities;

public class Dish
{
    public string Id { get; }
    public string Name { get; }
    public decimal Price { get; private set; }
    
    
    public Dish(string id, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id needed", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name needed", nameof(name));
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        
        Id = id.Trim();
        Name = name.Trim();
        Price = price;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0m) throw new ArgumentOutOfRangeException(nameof(newPrice), "Price must be >= 0.");
        Price = newPrice;
    }
}