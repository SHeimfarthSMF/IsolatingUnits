using Microsoft.Extensions.Logging;

namespace IsolatingUnits
{
    public class AccountingReport
    {
        private readonly DbContext _dbContext;
        private readonly FtpImport _ftpImport;
        private readonly FileStore _fileStore;
        private readonly ILogger<AccountingReport> _logger;

        public AccountingReport(DbContext dbContext, FtpImport ftpImport, FileStore fileStore, ILogger<AccountingReport> logger)
        {
            _dbContext = dbContext;
            _ftpImport = ftpImport;
            _fileStore = fileStore;
            _logger = logger;
        }

        public IEnumerable<AccountingReportItem> CreateReport(Guid customerId, DateTime businessDay)
        {
            _logger.LogInformation("Creating accounting report for customer {CustomerId} on {BusinessDay}", customerId, businessDay);
            var customer = _dbContext.GetCustomerById(customerId);
            if (customer is null)
                throw new ArgumentException("Customer not found", nameof(customerId));

            var result = new List<AccountingReportItem>();

            _logger.LogInformation("Getting invoices for customer {CustomerId} on {BusinessDay}", customerId, businessDay); 
            var invoices = _ftpImport.GetInvoicesForCustomer(customerId, businessDay);
            foreach (var invoice in invoices)
            {
                _logger.LogInformation("Processing invoice {InvoiceId} for customer {CustomerId} on {BusinessDay}", invoice.Id, customerId, businessDay);
                var receipt = _fileStore.FindReceipt(invoice.Id);

                var entry = new AccountingReportItem(invoice)
                {
                    InvoiceAmount = invoice.Items.Sum(i => i.ItemPrice * i.Quantity),
                    AlreadyPaid = receipt?.PaymentAmount ?? 0,
                };

                result.Add(entry);
            }

            return result;
        }
    }
}