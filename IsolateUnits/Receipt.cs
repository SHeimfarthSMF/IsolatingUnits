namespace IsolatingUnits;

public class Receipt
{
    public Guid InvoiceId { get; set; }

    public DateTime PaymentTime { get; set; }

    public decimal PaymentAmount { get; set; }
}