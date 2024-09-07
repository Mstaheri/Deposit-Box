using Application.Data.MoqData;
using Application.Services.BankAccounts.Commands.DeleteBankAccount;
using Application.Services.BankAccounts.Commands.UpdateBankAccount;
using Application.Services.BankAccounts.Queries.GetAllBankAccount;
using Application.Services.BankAccounts.Queries.GetBankAccount;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.BankAccountTest
{
    public class BankAccountTestQuery
    {
        private readonly BankAccountMoqData _moqData;
        private readonly Mock<IBankAccountRepositorieQuery> _repositorMoq;
        public BankAccountTestQuery()
        {
            _moqData = new BankAccountMoqData();
            _repositorMoq = new Mock<IBankAccountRepositorieQuery>();
        }
        [Fact]
        [Trait("Service", "BankAccount")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllBankAccountQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllBankAccountQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllBankAccountQueryHandler bankAccount = new GetAllBankAccountQueryHandler(
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getAllBankAccountQuery = new GetAllBankAccountQuery();
            var result = await bankAccount.Handle(getAllBankAccountQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<BankAccount>>>(result);
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
        [Trait("Service", "BankAccount")]
        [InlineData("1234323412341234")]
        [InlineData("1234123222341233")]
        [InlineData("1234123666341234")]
        [InlineData("12341111123412")]
        public async Task GetTestAsync(string accountNumber)
        {
            Mock<ILogger<GetBankAccountQueryHandler>> _loggerMoq = new Mock<ILogger<GetBankAccountQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<AccountNumber>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetBankAccountQueryHandler bankAccount = new GetBankAccountQueryHandler(
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getAllBankAccountQuery = new GetBankAccountQuery()
            {
                AccountNumber = accountNumber,
            };
            var result = await bankAccount.Handle(getAllBankAccountQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<BankAccount>>(result);
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
    }
}
