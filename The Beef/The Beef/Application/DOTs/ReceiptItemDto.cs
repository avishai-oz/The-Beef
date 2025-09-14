namespace The_Beef.Application.DOTs;

public record ReceiptItemDto(
    string DishName,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal);