using Application.Data.MoqData;
using Application.Services.BankSafeDocuments.Command.AddBankSafeDocuments;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.BankSafeDocumentTest
{
    public class BankSafeDocumentTestCommand
    {
        private readonly BankSafeDocumentMoqData _moqData;
        private readonly Mock<IBankSafeDocumentRepositorieCommand> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public BankSafeDocumentTestCommand()
        {
            _moqData = new BankSafeDocumentMoqData();
            _repositorMoq = new Mock<IBankSafeDocumentRepositorieCommand>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }

        [Fact]
        [Trait("Service", "BankSafeDocument")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddBankSafeDocumentsCommandHandler>> _loggerMoq = new Mock<ILogger<AddBankSafeDocumentsCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankSafeDocument>(), It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            AddBankSafeDocumentsCommandHandler bankSafeDocumentService = new AddBankSafeDocumentsCommandHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var addBankSafeDocumentsCommand = new AddBankSafeDocumentsCommand()
            {
                AccountNumber = data.AccountNumber,
                NameBankSafe = data.NameBankSafe,
                RegistrationDate = data.RegistrationDate,
                DueDate = data.DueDate,
                Withdrawal = data.Withdrawal,
                Deposit = data.Deposit,
                Situation = data.Situation,
            };
            var result = await bankSafeDocumentService.Handle(addBankSafeDocumentsCommand, It.IsAny<CancellationToken>());


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
