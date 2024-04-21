using Application.Data.MoqData;
using Application.Services;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services
{
    public class BankSafeDocumentTest
    {
        private readonly BankSafeDocumentMoqData _moqData;
        private readonly Mock<IBankSafeDocumentRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        private readonly Mock<ILogger<BankSafeDocumentService>> _loggerMoq;
        public BankSafeDocumentTest()
        {
            _moqData = new BankSafeDocumentMoqData();
            _repositorMoq = new Mock<IBankSafeDocumentRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
            _loggerMoq = new Mock<ILogger<BankSafeDocumentService>>();
        }

        [Fact]
        [Trait("Service", "BankSafeDocument")]
        public async Task AddTestAsync()
        {
            var data = await _moqData.Get();
            _repositorMoq.Setup(p => p.AddAsync(It.IsAny<BankSafeDocument>() , It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            BankSafeDocumentService bankSafeDocumentService = new BankSafeDocumentService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankSafeDocumentService.AddAsync(data);


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
        [Trait("Service", "BankSafeDocument")]
        public async Task GetAllTestAsync()
        {
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            BankSafeDocumentService bankSafeDocumentService = new BankSafeDocumentService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankSafeDocumentService.GetAllAsync();


            Assert.IsType<OperationResult<List<BankSafeDocument>>>(result);
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
        [Trait("Service", "BankSafeDocument")]
        [InlineData("3b2667f6-995c-49fb-a36b-195c965f442c")]
        [InlineData("1872255b-72dd-4cc6-84fa-50ab94677aca")]
        [InlineData("17466fd3-e221-413d-a419-dd4690bf7bc1")]
        public async Task GetTestAsync(Guid code)
        {
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Guid>() , It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            BankSafeDocumentService bankSafeDocumentService = new BankSafeDocumentService(_unitOfWorkMoq.Object,
                _repositorMoq.Object,
                _loggerMoq.Object);


            var result = await bankSafeDocumentService.GetAsync(code);


            Assert.IsType<OperationResult<BankSafeDocument>>(result);
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
