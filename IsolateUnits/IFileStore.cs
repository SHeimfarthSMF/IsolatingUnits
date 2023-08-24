namespace IsolatingUnits;

public interface IFileStore
{
    Receipt? FindReceipt(Guid invoiceId);
}