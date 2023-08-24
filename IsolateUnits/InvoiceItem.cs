namespace IsolatingUnits;

public class InvoiceItem
{
    public string ProductName { get; set; } = string.Empty;
    public decimal ItemPrice { get; set; }
    public decimal Quantity { get; set; }
}