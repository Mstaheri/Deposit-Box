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
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services
{
    public class UserAndNumberOfShareTest
    {
        private readonly UserAndNumberOfShareMoqData _moqData;
        private readonly Mock<IUserAndNumberOfShareRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        private readonly Mock<ILogger<UserAndNumberOfShareService>> _loggerMoq;
        public UserAndNumberOfShareTest()
        {
            _moqData = new UserAndNumberOfShareMoqData();
            _repositorMoq = new Mock<IUserAndNumberOfShareRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
            _loggerMoq = new Mock<ILogger<UserAndNumberOfShareService>>();
        }
        [Fact]
        [Trait("Services", "User")]
        public async Task AddTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.AddAsync(It.IsAny<UserAndNumberOfShare>()))
                .Returns(() => ValueTask.CompletedTask);
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.AddAsync(data);


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
        [InlineData("Omid","Mstaheri")]
        [InlineData("","Estaheri")]
        [InlineData("MST", "لیبل")]
        public async Task DeleteTestAsync(string nameBankSafe ,string userName)
        {
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<Name>(),It.IsAny<UserName>()))
                .Returns(() => Task.CompletedTask);
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.DeleteAsync(nameBankSafe,userName);


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
            _repositorMoq.Setup(repo => repo.GetNameBankAndUserNameAsync
            (It.IsAny<Name>() , It.IsAny<UserName>()))
                .Returns(_moqData.Get());
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.UpdateAsync(data);


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
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.GetAllAsync();


            Assert.IsType<OperationResult<List<UserAndNumberOfShare>>>(result);
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
        public async Task GetUserNameTestAsync(string userName)
        {
            _repositorMoq.Setup(p => p.GetUserNameAsync(It.IsAny<UserName>()))
                .Returns(_moqData.Get());
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.GetUserNameAsync(userName);


            Assert.IsType<OperationResult<UserAndNumberOfShare>>(result);
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
        [InlineData("محمد")]
        public async Task GetNameBankTestAsync(string name)
        {
            _repositorMoq.Setup(p => p.GetNameBankAsync(It.IsAny<Name>()))
                .Returns(_moqData.Get());
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.GetNameBankAsync(name);


            Assert.IsType<OperationResult<UserAndNumberOfShare>>(result);
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
        [InlineData("سعدابادی","estaheri")]
        [InlineData("salman","")]
        [InlineData("","MSI")]
        [InlineData("mamad","لیبل")]
        public async Task GetNameBankAndUserNameTestAsync(string nameBankSafe ,string userName)
        {
            _repositorMoq.Setup(p => p.GetNameBankAndUserNameAsync
            (It.IsAny<Name>() , It.IsAny<UserName>()))
                .Returns(_moqData.Get());
            UserAndNumberOfShareService userAndNumberOfShare = new UserAndNumberOfShareService(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var result = await userAndNumberOfShare.GetNameBankAndUserNameAsync(nameBankSafe, userName);


            Assert.IsType<OperationResult<UserAndNumberOfShare>>(result);
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
