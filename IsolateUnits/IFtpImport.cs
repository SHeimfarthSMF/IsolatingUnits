namespace IsolatingUnits;

public interface IFtpImport
{
    IReadOnlyList<Invoice> GetInvoicesForCustomer(Guid customerId, DateTime date);
}