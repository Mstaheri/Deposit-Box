using Application.Data.MoqData;
using Application.Services.BankSafeTransactions.Command.AddBankSafeTransaction;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeTransactionsRepositorie;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.BankSafeTransactionsTest
{
    public class BankSafeTransactionsTestCommand
    {
        private readonly BankSafeTransactionsMoqData _moqData;
        private readonly Mock<IBankSafeTransactionsRepositorieCommand> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;

        public BankSafeTransactionsTestCommand()
        {
            _moqData = new BankSafeTransactionsMoqData();
            _repositorMoq = new Mock<IBankSafeTransactionsRepositorieCommand>();
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
            var result = await bankSafeTransactionsService.Handle(addBankSafeTransactionCommand, It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult<Guid>>(result);
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
