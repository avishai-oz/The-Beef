using The_Beef.Application.DOTs;
using The_Beef.Application.Ports;
using The_Beef.Domain.Entities;

namespace The_Beef.Application.Services;

public class ReceiptService
{
    private readonly IMenuRepository _menu;
    private readonly IReceiptRenderer _renderer;

    public ReceiptService(IMenuRepository menu, IReceiptRenderer renderer)
    {
        _menu = menu ?? throw new ArgumentNullException(nameof(menu));
        _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
    }
    
    public async Task<ReceiptDto> GenerateAsync(Order order, decimal walletBefore, decimal walletAfter)
    {
        if (order is null) throw new ArgumentNullException(nameof(order));
        
        var ids = order.Items.Select(i => i.DishID).Distinct().ToList();
        var dishes = await _menu.FindByIds(ids);
        var nameById = dishes.ToDictionary(d => d.Id, d => d.Name, StringComparer.Ordinal);

        var items = order.Items.Select(i =>
            new ReceiptItemDto(
                nameById.TryGetValue(i.DishID, out var name) ? name : $"#{i.DishID}",
                i.Quantity,
                i.UnitPrice,
                i.LineTotal
            )).ToList();

        var discount = order.Subtotal - order.FinalAmount;

        return new ReceiptDto(
            OrderId       : order.OrderId,
            UserName      : order.userName, 
            TableId       : order.TableID,
            CreatedAt     : DateTime.UtcNow,
            Items         : items,
            Subtotal      : order.Subtotal,
            DiscountAmount: discount,
            FinalAmount   : order.FinalAmount,
            WalletBefore  : walletBefore,
            WalletAfter   : walletAfter
        );
    }
    public void Show(ReceiptDto dto)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));
        _renderer.Render(dto);
    }
}