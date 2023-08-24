using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace IsolatingUnits.Test
{
    public class AccountingReportTest
    {
        private static readonly DateTime TestDay = new (2021, 1, 1);
        private DbContext? _dbContext;
        private FtpImport? _ftpImport;
        private FileStore? _fileStore;
        private ILogger<AccountingReport>? _logger;
        private AccountingReport? _sut;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new DbContext();
            _ftpImport = new FtpImport();
            _fileStore = new FileStore();
            _logger = Substitute.For<ILogger<AccountingReport>>();
            
            _sut = new AccountingReport(_dbContext, _ftpImport, _fileStore, _logger);
        }

        [Test]
        public void CreateReport_CustomerAvailable_ShouldCreateReport()
        {
            // Act
            var result = _sut!.CreateReport(Guid.Parse("9ed58d64-e654-472b-9001-c9da00f081cf"), TestDay);

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void CreateReport_CustomerNotAvailable_ShouldThrow()
        {
            // Act/Assert
            Should.Throw<ArgumentException>(() => _sut!.CreateReport(Guid.Empty, TestDay));

        }
    }
}