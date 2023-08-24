namespace IsolatingUnits;

public class FileStore : IFileStore
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