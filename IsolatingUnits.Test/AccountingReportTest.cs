using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace IsolatingUnits.Test
{
    public class AccountingReportTest
    {
        private static readonly DateTime TestDay = new (2021, 1, 1);
        private Guid _existingCustomerId = Guid.Parse("9ed58d64-e654-472b-9001-c9da00f081cf");

        private IDbContext? _dbContext;
        private IFtpImport? _ftpImport;
        private IFileStore? _fileStore;
        private ILogger<AccountingReport>? _logger;
        private AccountingReport? _sut;

        [SetUp]
        public void SetUp()
        {
            _dbContext = Substitute.For<IDbContext>();
            _dbContext.GetCustomerById(_existingCustomerId).Returns(new Customer { Id = _existingCustomerId, FirstName = "Test Customer" });
            _ftpImport = Substitute.For<IFtpImport>();
            _fileStore = Substitute.For<IFileStore>();
            _logger = Substitute.For<ILogger<AccountingReport>>();
            
            _sut = new AccountingReport(_dbContext, _ftpImport, _fileStore, _logger);
        }

        [Test]
        public void CreateReport_CustomerAvailable_ShouldCreateReport()
        {
            // Act
            _ftpImport!.GetInvoicesForCustomer(_existingCustomerId, TestDay).Returns(new List<Invoice>
            {
                new Invoice
                {
                    Id = Guid.NewGuid(),
                    CustomerId = _existingCustomerId,
                    Items = new List<InvoiceItem>
                    {
                        new InvoiceItem { ItemPrice = 10, Quantity = 1 },
                        new InvoiceItem { ItemPrice = 20, Quantity = 2 },
                    }
                }
            }); 
            var result = _sut!.CreateReport(Guid.Parse("9ed58d64-e654-472b-9001-c9da00f081cf"), TestDay);

            // Assert
            result.ShouldNotBeNull();
            result.Single().TotalAmount.ShouldBe(50);
        }

        [Test]
        public void CreateReport_CustomerNotAvailable_ShouldThrow()
        {
            // Act/Assert
            Should.Throw<ArgumentException>(() => _sut!.CreateReport(Guid.Empty, TestDay));

        }

        [Test]
        public void CreateReport_ShouldGetInvoicesFromFtp()
        {
            // Act
            _sut!.CreateReport(_existingCustomerId, TestDay);   

            // Assert
            _ftpImport!.Received(1).GetInvoicesForCustomer(_existingCustomerId, TestDay);
        }

        [Test]
        public void CreateReport_ShouldFindReceiptFromFileStore()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();
            _ftpImport!.GetInvoicesForCustomer(_existingCustomerId, TestDay).Returns(new List<Invoice>
            {
                new Invoice
                {
                    Id = invoiceId,
                    CustomerId = _existingCustomerId,
                    Items = new List<InvoiceItem>
                    {
                        new InvoiceItem { ItemPrice = 10, Quantity = 1 },
                    }
                }
            }); 

            // Act
            _sut!.CreateReport(_existingCustomerId, TestDay);   

            // Assert
            _fileStore!.Received(1).FindReceipt(invoiceId);
        }
    }
}