using Application.Data.MoqData;
using Application.Services.Users.Commands.AddUser;
using Application.Services.Users.Commands.DeleteUser;
using Application.Services.Users.Commands.UpdateUser;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IUserRepositorie;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.UserServiceTest
{
    public class UserServiceTestCommand
    {
        private readonly UserMoqData _moqData;
        private readonly Mock<IUserRepositorieCommand> _repositorMoq;
        private readonly Mock<IUserRepositorieQuery> _repositorMoqQuery;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public UserServiceTestCommand()
        {
            _moqData = new UserMoqData();
            _repositorMoq = new Mock<IUserRepositorieCommand>();
            _repositorMoqQuery = new Mock<IUserRepositorieQuery>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }
        [Fact]
        [Trait("Services", "User")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddUserCommandHandler>> _loggerMoq = new Mock<ILogger<AddUserCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            AddUserCommandHandler user = new AddUserCommandHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);

            var addUserCommand = new AddUserCommand
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber,
                NationalIDNumber = data.NationalIDNumber,
                UserName = data.UserName,
                Password = data.Password,
            };
            var result = await user.Handle(addUserCommand, It.IsAny<CancellationToken>());


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
        [Trait("Services", "User")]
        [InlineData("Mstaheri")]
        [InlineData("Estaheri")]
        public async Task DeleteTestAsync(string userName)
        {
            Mock<ILogger<DeleteUserCommandHandler>> _loggerMoq = new Mock<ILogger<DeleteUserCommandHandler>>();
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.CompletedTask);
            DeleteUserCommandHandler user = new DeleteUserCommandHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var deleteUserCommand = new DeleteUserCommand { UserName = userName };
            var result = await user.Handle(deleteUserCommand, It.IsAny<CancellationToken>());


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
        [Trait("Services", "User")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateUserCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateUserCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoqQuery.Setup(repo => repo.GetAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateUserCommandHandler user = new UpdateUserCommandHandler(_repositorMoqQuery.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var updateUserCommand = new UpdateUserCommand
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber,
                NationalIDNumber = data.NationalIDNumber,
                UserName = data.UserName,
                Password = data.Password,
            };
            var result = await user.Handle(updateUserCommand, It.IsAny<CancellationToken>());


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
