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
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
        }

        [Fact]
        [Trait("Service", "BankAccount")]
        public async Task UpdateTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<string>()))
                .Returns(_moqData.Get());
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.UpdateAsync(data);


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
        }

        [Theory]
        [Trait("Service", "BankAccount")]
        [InlineData("5859831113124455")]
        [InlineData("5259834133126435")]
        public async Task DeleteTestAsync(string accountNumber)
        {
            _repositorMoq.Setup(p => p.DeleteAsync(It.IsAny<string>()))
                .Returns(() => Task.CompletedTask);
            BankAccountService bankAccount = new BankAccountService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankAccount.DeleteAsync(accountNumber);


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
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


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult<List<BankAccount>>>(result);
        }

    }
}
