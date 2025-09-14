namespace The_Beef.Application.DOTs;

public record ReceiptDto(
    string OrderId,
    string UserName,
    string TableId,
    DateTime CreatedAt,
    IReadOnlyList<ReceiptItemDto> Items,
    decimal Subtotal,
    decimal DiscountAmount,
    decimal FinalAmount,
    decimal WalletBefore,
    decimal WalletAfter);