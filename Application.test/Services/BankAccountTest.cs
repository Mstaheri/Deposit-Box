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
using Domain.OperationResults;
using Domain.ValueObjects;

namespace Application.test.Services
{
    public class BankAccountTest
    {
        private readonly BankAccountMoqData _moqData;
        private readonly Mock<IBankAccountRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        private readonly Mock<ILogger<BankAccountService>> _loggerMoq;
        public BankAccountTest()
        {
            _moqData = new BankAccountMoqData();
            _repositorMoq = new Mock<IBankAccountRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
            _loggerMoq = new Mock<ILogger<BankAccountService>>();
        }

        [Fact]
        [Trait("Service" , "BankAccount")]
        public async Task AddTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankAccount>()))
                .Returns(() => ValueTask.CompletedTask);
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.AddAsync(data);


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
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<AccountNumber>()))
                .Returns(_moqData.Get());
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.UpdateAsync(data);


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
            _repositorMoq.Setup(p => p.DeleteAsync(It.IsAny<AccountNumber>()))
                .Returns(() => Task.CompletedTask);
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.DeleteAsync(accountNumber);


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
            _repositorMoq.Setup(p => p.GetAllAsync())
                .Returns(_moqData.GetAll());
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.GetAllAsync();


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
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<AccountNumber>()))
                .Returns(_moqData.Get());
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.GetAsync(accountNumber);


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
