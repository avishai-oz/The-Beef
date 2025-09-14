using System.Globalization;
using The_Beef.Application.DOTs;
using The_Beef.Application.Ports;

namespace The_Beef.Infrastructure.Rendering;

public class ConsoleReceiptRenderer : IReceiptRenderer
{
    public void Render(ReceiptDto dto)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));

        var c = CultureInfo.InvariantCulture; 
        Console.WriteLine("===== RECEIPT =====");
        Console.WriteLine($"Order:   {dto.OrderId}");
        Console.WriteLine($"User:    {dto.UserName}");
        Console.WriteLine($"Table:   {dto.TableId}");
        Console.WriteLine($"When:    {dto.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC");
        Console.WriteLine("-------------------");
        foreach (var item in dto.Items)
        {
            var unit = item.UnitPrice.ToString("0.00", c);
            var line = item.LineTotal.ToString("0.00", c);
            Console.WriteLine($"{item.DishName} x{item.Quantity} @ {unit} = {line}");
        }
        Console.WriteLine("-------------------");
        Console.WriteLine($"Subtotal: {dto.Subtotal.ToString("0.00", c)}");
        Console.WriteLine($"Discount: {dto.DiscountAmount.ToString("0.00", c)}");
        Console.WriteLine($"TOTAL:    {dto.FinalAmount.ToString("0.00", c)}");
        Console.WriteLine($"Wallet: Befoul: {dto.WalletBefore.ToString("0.00", c)} → After: {dto.WalletAfter.ToString("0.00", c)}");
        Console.WriteLine("===================");
    }
}