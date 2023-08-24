using Microsoft.Extensions.Logging;

namespace IsolatingUnits
{
    public class AccountingReport
    {
        public Customer Customer { get; }
        public DateTime BusinessDate { get; }
        public IList<AccountingReportItem> Report { get; private set; } = new List<AccountingReportItem>();

        public AccountingReport(Customer customer, DateTime businessDate)
        {
            Customer = customer;
            BusinessDate = businessDate;
        }

        public void CreateReport(IReadOnlyList<Invoice> invoices, IReadOnlyList<Receipt> receipts)
        {
            Report = new List<AccountingReportItem>();
            foreach (var invoice in invoices)
            {
                var receipt = receipts.FirstOrDefault(x=>x.InvoiceId == invoice.Id);

                var entry = new AccountingReportItem(invoice)
                {
                    InvoiceAmount = invoice.Items.Sum(i => i.ItemPrice * i.Quantity),
                    AlreadyPaid = receipt?.PaymentAmount ?? 0,
                };

                Report.Add(entry);
            }
        }
    }
}