using Application.Data.MoqData;
using Application.Services.BankAccounts.Commands.AddBankAccount;
using Application.Services.BankAccounts.Commands.DeleteBankAccount;
using Application.Services.BankAccounts.Commands.UpdateBankAccount;
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
    public class BankAccountTestCommand
    {
        private readonly BankAccountMoqData _moqData;
        private readonly Mock<IBankAccountRepositorieCommand> _repositorMoq;
        private readonly Mock<IBankAccountRepositorieQuery> _repositorMoqQuery;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public BankAccountTestCommand()
        {
            _moqData = new BankAccountMoqData();
            _repositorMoq = new Mock<IBankAccountRepositorieCommand>();
            _repositorMoqQuery = new Mock<IBankAccountRepositorieQuery>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }

        [Fact]
        [Trait("Service", "BankAccount")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddBankAccountCommandHandler>> _loggerMoq = new Mock<ILogger<AddBankAccountCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankAccount>(), It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            AddBankAccountCommandHandler bankAccount = new AddBankAccountCommandHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var addBankAccountCommand = new AddBankAccountCommand()
            {
                AccountNumber = data.AccountNumber,
                UserName = data.UserName,
                AccountName = data.AccountName,
                BankName = data.BankName,
                Description = data.Description
            };
            var result = await bankAccount.Handle(addBankAccountCommand, It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }
        [Theory]
        [Trait("Service", "BankAccount")]
        [InlineData("5859831113124455")]
        [InlineData("52598341331264")]
        public async Task DeleteTestAsync(string accountNumber)
        {
            Mock<ILogger<DeleteBankAccountCommandHandler>> _loggerMoq = new Mock<ILogger<DeleteBankAccountCommandHandler>>();
            _repositorMoq.Setup(p => p.DeleteAsync(It.IsAny<AccountNumber>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.CompletedTask);
            DeleteBankAccountCommandHandler bankAccount = new DeleteBankAccountCommandHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var deleteBankAccountCommand = new DeleteBankAccountCommand()
            {
                AccountNumber = accountNumber
            };
            var result = await bankAccount.Handle(deleteBankAccountCommand, It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }
        [Fact]
        [Trait("Service", "BankAccount")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateBankAccountCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateBankAccountCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoqQuery.Setup(p => p.GetAsync(It.IsAny<AccountNumber>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateBankAccountCommandHandler bankAccount = new UpdateBankAccountCommandHandler(_unitOfWorkMoq.Object,
                _repositorMoqQuery.Object,
                _loggerMoq.Object);


            var updateBankAccountCommand = new UpdateBankAccountCommand()
            {
                AccountNumber = data.AccountNumber,
                UserName = data.UserName,
                AccountName = data.AccountName,
                BankName = data.BankName,
                Description = data.Description
            };
            var result = await bankAccount.Handle(updateBankAccountCommand, It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }


    }
}
