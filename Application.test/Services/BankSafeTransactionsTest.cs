using Application.Data.MoqData;
using Application.Services;
using Application.Services.BankSafeDocuments.Queries.GetAllBankSafeDocuments;
using Application.Services.BankSafes.Queries.GetAllBankSafe;
using Application.Services.BankSafeTransactions.Command.AddBankSafeTransaction;
using Application.Services.BankSafeTransactions.Queries.GetAllBankSafeTransaction;
using Application.Services.BankSafeTransactions.Queries.GetBankSafeTransaction;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services
{
    public class BankSafeTransactionsTest
    {
        private readonly BankSafeTransactionsMoqData _moqData;
        private readonly Mock<IBankSafeTransactionsRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        
        public BankSafeTransactionsTest()
        {
            _moqData = new BankSafeTransactionsMoqData();
            _repositorMoq = new Mock<IBankSafeTransactionsRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }

        [Fact]
        [Trait("Service", "BankSafeTransactions")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddBankSafeTransactionCommandHandler>> _loggerMoq = new Mock<ILogger<AddBankSafeTransactionCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankSafeTransaction>(), It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            AddBankSafeTransactionCommandHandler bankSafeTransactionsService = new AddBankSafeTransactionCommandHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var addBankSafeTransactionCommand = new AddBankSafeTransactionCommand()
            {
                AccountNumber = data.AccountNumber,
                NameBankSafe = data.NameBankSafe,
                Withdrawal = data.Withdrawal,
                Deposit = data.Deposit,
            };
            var result = await bankSafeTransactionsService.Handle(addBankSafeTransactionCommand , It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult<Guid>>(result);
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
        [Trait("Service", "BankSafeTransactions")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllBankSafeTransactionQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllBankSafeTransactionQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllBankSafeTransactionQueryHandler bankSafeTransactionsService = new GetAllBankSafeTransactionQueryHandler(
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getAllBankSafeTransactionQuery = new GetAllBankSafeTransactionQuery();
            var result = await bankSafeTransactionsService.Handle(getAllBankSafeTransactionQuery , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<BankSafeTransaction>>>(result);
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
        [Trait("Service", "BankSafeTransactions")]
        [InlineData("3b2667f6-995c-49fb-a36b-195c965f442c")]
        [InlineData("1872255b-72dd-4cc6-84fa-50ab94677aca")]
        [InlineData("17466fd3-e221-413d-a419-dd4690bf7bc1")]
        public async Task GetTestAsync(Guid code)
        {
            Mock<ILogger<GetBankSafeTransactionCommandHandler>> _loggerMoq = new Mock<ILogger<GetBankSafeTransactionCommandHandler>>();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetBankSafeTransactionCommandHandler bankSafeTransactionsService = new GetBankSafeTransactionCommandHandler(
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getBankSafeTransactionCommand = new GetBankSafeTransactionCommand()
            {
                Code = code,
            };
            var result = await bankSafeTransactionsService.Handle(getBankSafeTransactionCommand , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<BankSafeTransaction>>(result);
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

    }
}

