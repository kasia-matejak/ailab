namespace AiLab.Application.Model;

public class StockModel
{
    public ulong Id { get; set; }
    public ulong ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public ulong SizeId { get; set; }
    public string SizeName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
