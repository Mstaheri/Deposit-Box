using Application.UnitOfWork;
using Domain.IRepositories;
using Application.Services;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.MoqData;
using Domain.ValueObjects;
using System.Threading;
using Domain.Exceptions;
using Application.Services.BankAccounts.Commands.AddBankAccount;
using Application.Services.BankAccounts.Commands.UpdateBankAccount;
using Application.Services.BankAccounts.Commands.DeleteBankAccount;
using Application.Services.BankAccounts.Queries.GetAllBankAccount;
using Application.Services.BankAccounts.Queries.GetBankAccount;

namespace Application.test.Services
{
    public class BankAccountTest
    {
        private readonly BankAccountMoqData _moqData;
        private readonly Mock<IBankAccountRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public BankAccountTest()
        {
            _moqData = new BankAccountMoqData();
            _repositorMoq = new Mock<IBankAccountRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }

        [Fact]
        [Trait("Service" , "BankAccount")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddBankAccountCommandHandler>> _loggerMoq = new Mock<ILogger<AddBankAccountCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankAccount>() , It.IsAny<CancellationToken>()))
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
        [Trait("Service", "BankAccount")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateBankAccountCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateBankAccountCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<AccountNumber>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateBankAccountCommandHandler bankAccount = new UpdateBankAccountCommandHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
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
        [Trait("Service", "BankAccount")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllBankAccountQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllBankAccountQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllBankAccountQueryHandler bankAccount = new GetAllBankAccountQueryHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getAllBankAccountQuery = new GetAllBankAccountQuery();
            var result = await bankAccount.Handle(getAllBankAccountQuery , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<BankAccount>>>(result);
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
            GetBankAccountQueryHandler bankAccount = new GetBankAccountQueryHandler(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getAllBankAccountQuery = new GetBankAccountQuery()
            {
                AccountNumber = accountNumber,
            };
            var result = await bankAccount.Handle(getAllBankAccountQuery , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<BankAccount>>(result);
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
