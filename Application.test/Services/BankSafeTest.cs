using Application.Data.MoqData;
using Application.Services;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.OperationResults;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services
{
    public class BankSafeTest
    {
        private readonly BankSafeMoqData _moqData;
        private readonly Mock<IBankSafeRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        private readonly Mock<ILogger<BankSafeService>> _loggerMoq;
        public BankSafeTest()
        {
            _moqData = new BankSafeMoqData();
            _repositorMoq = new Mock<IBankSafeRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
            _loggerMoq = new Mock<ILogger<BankSafeService>>();
        }
        [Fact]
        [Trait("Service", "BankSafe")]
        public async Task AddTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankSafe>()))
                .Returns(() => ValueTask.CompletedTask);
            BankSafeService bankSafe = new BankSafeService(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var result = await bankSafe.AddAsync(data);


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
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Name>()))
                .Returns(_moqData.Get());
            BankSafeService bankSafe = new BankSafeService(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var result = await bankSafe.UpdateAsync(data);


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
        [InlineData("Omid")]
        [InlineData("")]
        public async Task DeleteTestAsync(string name)
        {
            _repositorMoq.Setup(p => p.DeleteAsync(It.IsAny<Name>()))
                .Returns(() => Task.CompletedTask);
            BankSafeService bankSafe = new BankSafeService(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var result = await bankSafe.DeleteAsync(name);


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
            BankSafeService bankSafe = new BankSafeService(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var result = await bankSafe.GetAllAsync();


            Assert.IsType<OperationResult<List<BankSafe>>>(result);
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
        [InlineData("omid")]
        [InlineData("")]
        [InlineData("MSI")]
        [InlineData(":D")]
        public async Task GetTestAsync(string name)
        {
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Name>()))
                .Returns(_moqData.Get());
            BankSafeService bankSafe = new BankSafeService(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var result = await bankSafe.GetAsync(name);


            Assert.IsType<OperationResult<BankSafe>>(result);
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
        [Fact]
        public async Task InventoryTestAsync()
        {
            _repositorMoq.Setup(p => p.Inventory())
                .Returns(It.IsAny<Task<decimal>>);
            BankSafeService bankSafe = new BankSafeService(_repositorMoq.Object
                , _unitOfWorkMoq.Object, _loggerMoq.Object);


            var result = await bankSafe.Inventory();


            Assert.IsType<OperationResult<decimal>>(result);
            if (result.Success)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Equal(result.Data , -1);
            }

        }
    }
}
