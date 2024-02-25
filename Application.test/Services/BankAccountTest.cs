using Application.IRepositories;
using Application.Models;
using Application.Models.MoqData;
using Application.Services;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            var result = await bankAccount.Add(data);


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
        }
    }
}
