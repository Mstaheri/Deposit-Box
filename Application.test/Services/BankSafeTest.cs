using Application.Data.MoqData;
using Application.Services;
using Application.Services.BankAccounts.Queries.GetAllBankAccount;
using Application.Services.BankSafes.Commands.AddBankSafe;
using Application.Services.BankSafes.Commands.DeleteBankSafe;
using Application.Services.BankSafes.Commands.UpdateBankSafe;
using Application.Services.BankSafes.Queries.GetAllBankSafe;
using Application.Services.BankSafes.Queries.GetBankSafe;
using Application.Services.BankSafes.Queries.InventoryBankSafe;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.test.Services
{
    public class BankSafeTest
    {
        private readonly BankSafeMoqData _moqData;
        private readonly Mock<IBankSafeRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public BankSafeTest()
        {
            _moqData = new BankSafeMoqData();
            _repositorMoq = new Mock<IBankSafeRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }
        [Fact]
        [Trait("Service", "BankSafe")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddBankSafeCommandHandler>> _loggerMoq = new Mock<ILogger<AddBankSafeCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.Add(It.IsAny<BankSafe>()));
            AddBankSafeCommandHandler bankSafe = new AddBankSafeCommandHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var addBankSafeCommand = new AddBankSafeCommand()
            {
                Name = data.Name,
                SharePrice = data.SharePrice,
            };
            var result = await bankSafe.Handle(addBankSafeCommand , It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }
        [Fact]
        [Trait("Service", "BankSafe")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateBankSafeCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateBankSafeCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Name>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateBankSafeCommandHandler bankSafe = new UpdateBankSafeCommandHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var updateBankSafeCommand = new UpdateBankSafeCommand()
            {
                Name = data.Name,
                SharePrice = data.SharePrice,
            };
            var result = await bankSafe.Handle(updateBankSafeCommand, It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }

        [Theory]
        [Trait("Service", "BankSafe")]
        [InlineData("Omid")]
        [InlineData("")]
        public async Task DeleteTestAsync(string name)
        {
            Mock<ILogger<DeleteBankSafeCommandHandler>> _loggerMoq = new Mock<ILogger<DeleteBankSafeCommandHandler>>();
            _repositorMoq.Setup(p => p.DeleteAsync(It.IsAny<Name>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.CompletedTask);
            DeleteBankSafeCommandHandler bankSafe = new DeleteBankSafeCommandHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var deleteBankSafeCommand = new DeleteBankSafeCommand()
            {
                Name = name,
            };
            var result = await bankSafe.Handle(deleteBankSafeCommand, It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }

        [Fact]
        [Trait("Service", "BankSafe")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllBankSafeQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllBankSafeQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllBankSafeQueryHandler bankSafe = new GetAllBankSafeQueryHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var getAllBankSafeQuery = new GetAllBankSafeQuery();
            var result = await bankSafe.Handle(getAllBankSafeQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<BankSafe>>>(result);
            if (result.Success)
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
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var getBankSafeQuery = new GetBankSafeQuery()
            { Name= name };
            var result = await bankSafe.Handle(getBankSafeQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<BankSafe>>(result);
            if (result.Success)
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
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var inventoryBankSafeQuery = new InventoryBankSafeQuery();
            var result = await bankSafe.Handle(inventoryBankSafeQuery , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<decimal>>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Equal(result.Data , -1);
            }

        }
    }
}
