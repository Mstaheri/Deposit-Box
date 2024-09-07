using Application.Data.MoqData;
using Application.Services.BankSafes.Queries.GetAllBankSafe;
using Application.Services.BankSafes.Queries.GetBankSafe;
using Application.Services.BankSafes.Queries.InventoryBankSafe;
using Application.UnitOfWork;
using Castle.Core.Logging;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.BankSafeTest
{
    public class BankSafeTestQuery
    {
        private readonly BankSafeMoqData _moqData;
        private readonly Mock<IBankSafeRepositorieQuery> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public BankSafeTestQuery()
        {
            _moqData = new BankSafeMoqData();
            _repositorMoq = new Mock<IBankSafeRepositorieQuery>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }
        [Fact]
        [Trait("Service", "BankSafe")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllBankSafeQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllBankSafeQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllBankSafeQueryHandler bankSafe = new GetAllBankSafeQueryHandler(_repositorMoq.Object
                , _loggerMoq.Object);


            var getAllBankSafeQuery = new GetAllBankSafeQuery();
            var result = await bankSafe.Handle(getAllBankSafeQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<BankSafe>>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }
        [Theory]
        [Trait("Service", "BankSafe")]
        [InlineData("omid")]
        [InlineData("")]
        [InlineData("MSI")]
        [InlineData(":D")]
        public async Task GetTestAsync(string name)
        {
            Mock<ILogger<GetBankSafeQueryHandler>> _loggerMoq = new Mock<ILogger<GetBankSafeQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Name>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetBankSafeQueryHandler bankSafe = new GetBankSafeQueryHandler(_repositorMoq.Object
                , _loggerMoq.Object);


            var getBankSafeQuery = new GetBankSafeQuery()
            { Name = name };
            var result = await bankSafe.Handle(getBankSafeQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<BankSafe>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }
        [Fact]
        [Trait("Service", "BankSafe")]
        public async Task InventoryTestAsync()
        {
            Mock<ILogger<InventoryBankSafeQueryHandler>> _loggerMoq = new Mock<ILogger<InventoryBankSafeQueryHandler>>();
            _repositorMoq.Setup(p => p.Inventory(It.IsAny<CancellationToken>()))
                .Returns(It.IsAny<Task<decimal>>);
            InventoryBankSafeQueryHandler bankSafe = new InventoryBankSafeQueryHandler(_repositorMoq.Object
                , _loggerMoq.Object);


            var inventoryBankSafeQuery = new InventoryBankSafeQuery();
            var result = await bankSafe.Handle(inventoryBankSafeQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<decimal>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Equal(result.Data, -1);
            }

        }
    }
}
