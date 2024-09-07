using Application.Data.MoqData;
using Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Commands.DeleteUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Commands.UpdateUserAndNumberOfShare;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.UserAndNumberOfShareTest
{
    public class UserAndNumberOfShareTestCommand
    {
        private readonly UserAndNumberOfShareMoqData _moqData;
        private readonly Mock<IUserAndNumberOfShareRepositorieCommand> _repositorMoq;
        private readonly Mock<IUserAndNumberOfShareRepositorieQuery> _repositorMoqQuery;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public UserAndNumberOfShareTestCommand()
        {
            _moqData = new UserAndNumberOfShareMoqData();
            _repositorMoq = new Mock<IUserAndNumberOfShareRepositorieCommand>();
            _repositorMoqQuery = new Mock<IUserAndNumberOfShareRepositorieQuery>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }
        [Fact]
        [Trait("Services", "User")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddUserAndNumberOfShareCommandHandler>> _loggerMoq = new Mock<ILogger<AddUserAndNumberOfShareCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.AddAsync(It.IsAny<UserAndNumberOfShare>(), It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            AddUserAndNumberOfShareCommandHandler userAndNumberOfShare = new AddUserAndNumberOfShareCommandHandler(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var addUserAndNumberOfShareCommand = new AddUserAndNumberOfShareCommand()
            {
                NameBankSafe = data.NameBankSafe,
                UserName = data.UserName,
                NumberOfShares = data.NumberOfShares,
            };
            var result = await userAndNumberOfShare.Handle(addUserAndNumberOfShareCommand, It.IsAny<CancellationToken>());


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
        [Trait("Services", "UserAndNumberOfShare")]
        [InlineData("Omid", "Mstaheri")]
        [InlineData("", "Estaheri")]
        [InlineData("MST", "لیبل")]
        public async Task DeleteTestAsync(string nameBankSafe, string userName)
        {
            Mock<ILogger<DeleteUserAndNumberOfShareCommandHandler>> _loggerMoq = new Mock<ILogger<DeleteUserAndNumberOfShareCommandHandler>>();
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<Name>(), It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.CompletedTask);
            DeleteUserAndNumberOfShareCommandHandler userAndNumberOfShare = new DeleteUserAndNumberOfShareCommandHandler(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var deleteUserAndNumberOfShareCommand = new DeleteUserAndNumberOfShareCommand()
            { NameBankSafe = nameBankSafe, UserName = userName };
            var result = await userAndNumberOfShare.Handle(deleteUserAndNumberOfShareCommand, It.IsAny<CancellationToken>());


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
        [Trait("Services", "UserAndNumberOfShare")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateUserAndNumberOfShareCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateUserAndNumberOfShareCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoqQuery.Setup(repo => repo.GetNameBankAndUserNameAsync
            (It.IsAny<Name>(), It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateUserAndNumberOfShareCommandHandler userAndNumberOfShare = new UpdateUserAndNumberOfShareCommandHandler(_unitOfWorkMoq.Object
                , _repositorMoqQuery.Object
                , _loggerMoq.Object);


            var updateUserAndNumberOfShareCommand = new UpdateUserAndNumberOfShareCommand()
            {
                NameBankSafe = data.NameBankSafe,
                UserName = data.UserName,
                NumberOfShares = data.NumberOfShares,
            };
            var result = await userAndNumberOfShare.Handle(updateUserAndNumberOfShareCommand, It.IsAny<CancellationToken>());


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
