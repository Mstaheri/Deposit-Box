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
using Domain.ValueObjects;
using Domain.Exceptions;
using Application.Services.Users.Commands.AddUser;
using Application.Services.Users.Commands.DeleteUser;
using Application.Services.Users.Commands.UpdateUser;
using Application.Services.Users.Queries.GetAllUser;
using Application.Services.Users.Queries.GetUser;

namespace Application.test.Services
{
    public class UserServiceTest
    {
        private readonly UserMoqData _moqData;
        private readonly Mock<IUserRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public UserServiceTest()
        {
            _moqData = new UserMoqData();
            _repositorMoq = new Mock<IUserRepositorie>();
            _unitOfWorkMoq= new Mock<IUnitOfWork>();
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
                FirstName= data.FirstName,
                LastName= data.LastName,
                PhoneNumber= data.PhoneNumber,
                NationalIDNumber= data.NationalIDNumber,
                UserName = data.UserName,
                Password = data.Password,
            };
            var result = await user.Handle(addUserCommand, It.IsAny<CancellationToken>());


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
            Mock<ILogger<DeleteUserCommandHandler>> _loggerMoq = new Mock<ILogger<DeleteUserCommandHandler>>();
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.CompletedTask);
            DeleteUserCommandHandler user = new DeleteUserCommandHandler(_repositorMoq.Object
                , _unitOfWorkMoq.Object
                , _loggerMoq.Object);


            var deleteUserCommand = new DeleteUserCommand { UserName= userName };
            var result = await user.Handle(deleteUserCommand, It.IsAny<CancellationToken>());


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
            Mock<ILogger<UpdateUserCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateUserCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.GetAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateUserCommandHandler user = new UpdateUserCommandHandler(_repositorMoq.Object
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
            Mock<ILogger<GetAllUserQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllUserQueryHandler>>();
            _repositorMoq.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllUserQueryHandler user = new GetAllUserQueryHandler(_repositorMoq.Object
                , _loggerMoq.Object);


            var getAllUserCommand = new GetAllUserQuery();
            var result = await user.Handle(getAllUserCommand,It.IsAny<CancellationToken>());


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
            Mock<ILogger<GetUserQueryHandler>> _loggerMoq = new Mock<ILogger<GetUserQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetUserQueryHandler user = new GetUserQueryHandler(_repositorMoq.Object
                , _loggerMoq.Object);


            var getUserCommand = new GetUserQuery
            { UserName= userName };
            var result = await user.Handle(getUserCommand , It.IsAny<CancellationToken>());


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
