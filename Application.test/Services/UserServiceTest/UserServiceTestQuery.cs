using Application.Data.MoqData;
using Application.Services.Users.Queries.GetAllUser;
using Application.Services.Users.Queries.GetUser;
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
    public class UserServiceTestQuery
    {
        private readonly UserMoqData _moqData;
        private readonly Mock<IUserRepositorieQuery> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public UserServiceTestQuery()
        {
            _moqData = new UserMoqData();
            _repositorMoq = new Mock<IUserRepositorieQuery>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
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
            var result = await user.Handle(getAllUserCommand, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<User>>>(result);
            if (result.IsSuccess)
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
            { UserName = userName };
            var result = await user.Handle(getUserCommand, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<User>>(result);
            if (result.IsSuccess)
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
