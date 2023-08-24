using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace IsolatingUnits.Test
{
    public class AccountingReportTest
    {
        private static readonly DateTime TestDay = new (2021, 1, 1);
        private readonly Guid _existingCustomerId = Guid.Parse("9ed58d64-e654-472b-9001-c9da00f081cf");

        [Test]
        public void CreateReport_OneInvoice_ShouldCreateReport()
        {
            // Arrange
            var sut = new AccountingReport(new Customer() { Id = _existingCustomerId }, TestDay);
            var invoices = new List<Invoice>
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
            };
            var receipts = new List<Receipt>{};

            // Act
            sut.CreateReport(invoices, receipts);

            // Assert
            sut.Report[0].InvoiceAmount.ShouldBe(50);
        }
    }
}