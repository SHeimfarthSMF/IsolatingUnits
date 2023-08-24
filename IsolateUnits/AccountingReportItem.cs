namespace IsolatingUnits;

public record AccountingReportItem
{
    public AccountingReportItem(Invoice invoice)
    {
        Invoice = invoice;
    }

    public Invoice Invoice { get; set; }

    public decimal InvoiceAmount { get; set; }

    public decimal AlreadyPaid { get; set; }

    public decimal TotalAmount => InvoiceAmount - AlreadyPaid;
}