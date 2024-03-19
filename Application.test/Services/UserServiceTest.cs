using Application.UnitOfWork;
using Domain.IRepositories;
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
using System.Threading;
using System.Threading.Tasks;

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
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
        }

        [Theory]
        [Trait("Services", "User")]
        [InlineData("Mstaheri")]
        [InlineData("Estaheri")]
        public async Task DeleteTestAsync(string userName)
        {
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<string>()))
                .Returns(() => Task.CompletedTask);
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.DeleteAsync(userName);


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
        }

        [Fact]
        [Trait("Services", "User")]
        public async Task UpdateTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.GetAsync(It.IsAny<string>()))
                .Returns(_moqData.Get());
            UserService user = new UserService(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var result = await user.UpdateAsync(data);


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult>(result);
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


            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsType<OperationResult<List<User>>>(result);
        }
    }
}
