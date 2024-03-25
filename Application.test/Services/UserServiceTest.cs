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
using System.Threading;
using System.Threading.Tasks;
using Application.Data.MoqData;
using Domain.OperationResults;
using Domain.ValueObjects;

namespace Application.test.Services
{
    public class UserServiceTest
    {
        private readonly UserMoqData _moqData;
        private readonly Mock<IUserRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        private readonly Mock<ILogger<UserService>> _loggerMoq;
        public UserServiceTest()
        {
            _moqData = new UserMoqData();
            _repositorMoq = new Mock<IUserRepositorie>();
            _unitOfWorkMoq= new Mock<IUnitOfWork>();
            _loggerMoq= new Mock<ILogger<UserService>>();
        }
        [Fact]
        [Trait("Services", "User")]
        public async Task AddTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Returns(() => ValueTask.CompletedTask);
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.AddAsync(data);


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
        [Trait("Services", "User")]
        [InlineData("Mstaheri")]
        [InlineData("Estaheri")]
        public async Task DeleteTestAsync(string userName)
        {
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<UserName>()))
                .Returns(() => Task.CompletedTask);
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.DeleteAsync(userName);


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
        [Trait("Services", "User")]
        public async Task UpdateTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.GetAsync(It.IsAny<UserName>()))
                .Returns(_moqData.Get());
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.UpdateAsync(data);


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
        [Trait("Services", "User")]
        public async Task GetAllTestAsync()
        {
            _repositorMoq.Setup(repo => repo.GetAllAsync())
                .Returns(_moqData.GetAll());
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.GetAllAsync();


            Assert.IsType<OperationResult<List<User>>>(result);
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
        [InlineData("estaheri")]
        [InlineData("")]
        [InlineData("MSI")]
        [InlineData("لیبل")]
        public async Task GetTestAsync(string userName)
        {
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<UserName>()))
                .Returns(_moqData.Get());
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.GetAsync(userName);


            Assert.IsType<OperationResult<User>>(result);
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
