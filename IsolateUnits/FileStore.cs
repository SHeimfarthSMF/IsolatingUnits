namespace IsolatingUnits;

public class FileStore
{
    public Receipt? FindReceipt(Guid invoiceId)
    {
        return new Receipt()
        {
            InvoiceId = invoiceId,
            PaymentTime = DateTime.Today,
            PaymentAmount = 100
        };
    }
}