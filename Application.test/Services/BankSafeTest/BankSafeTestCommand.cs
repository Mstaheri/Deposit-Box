using Application.Data.MoqData;
using Application.Services.BankSafes.Commands.AddBankSafe;
using Application.Services.BankSafes.Commands.DeleteBankSafe;
using Application.Services.BankSafes.Commands.UpdateBankSafe;
using Application.UnitOfWork;
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
    public class BankSafeTestCommand
    {
        private readonly BankSafeMoqData _moqData;
        private readonly Mock<IBankSafeRepositorieCommand> _repositorMoq;
        private readonly Mock<IBankSafeRepositorieQuery> _repositorMoqQuery;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public BankSafeTestCommand()
        {
            _moqData = new BankSafeMoqData();
            _repositorMoq = new Mock<IBankSafeRepositorieCommand>();
            _repositorMoqQuery = new Mock<IBankSafeRepositorieQuery>();
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
            var result = await bankSafe.Handle(addBankSafeCommand, It.IsAny<CancellationToken>());


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
        [Trait("Service", "BankSafe")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateBankSafeCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateBankSafeCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoqQuery.Setup(p => p.GetAsync(It.IsAny<Name>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateBankSafeCommandHandler bankSafe = new UpdateBankSafeCommandHandler(_repositorMoqQuery.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var updateBankSafeCommand = new UpdateBankSafeCommand()
            {
                Name = data.Name,
                SharePrice = data.SharePrice,
            };
            var result = await bankSafe.Handle(updateBankSafeCommand, It.IsAny<CancellationToken>());


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
