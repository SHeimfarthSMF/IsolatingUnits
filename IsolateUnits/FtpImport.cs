namespace IsolatingUnits;

public class FtpImport : IFtpImport
{
    public IReadOnlyList<Invoice> GetInvoicesForCustomer(Guid customerId, DateTime date)
    {
        return new List<Invoice>
        {
            new()
            {
                CustomerId = customerId,
                Items = new List<InvoiceItem>
                {
                    new()
                    {
                        ProductName = "Product 1",
                        ItemPrice = 10,
                        Quantity = 1
                    },
                    new()
                    {
                        ProductName = "Product 2",
                        ItemPrice = 20,
                        Quantity = 2
                    }
                }
            }
        };
    }
}