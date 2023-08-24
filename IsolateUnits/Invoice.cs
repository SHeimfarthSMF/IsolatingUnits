namespace IsolatingUnits;

public class Invoice
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public DateTime BillingDate { get; set; }

    public List<InvoiceItem> Items { get; set; } = new();
}