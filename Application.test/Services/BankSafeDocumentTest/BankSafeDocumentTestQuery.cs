using Application.Data.MoqData;
using Application.Services.BankSafeDocuments.Queries.GetAllBankSafeDocuments;
using Application.Services.BankSafeDocuments.Query.GetBankSafeDocuments;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.test.Services.BankSafeDocumentTest
{
    public class BankSafeDocumentTestQuery
    {
        private readonly BankSafeDocumentMoqData _moqData;
        private readonly Mock<IBankSafeDocumentRepositorieQuery> _repositorMoq;
        public BankSafeDocumentTestQuery()
        {
            _moqData = new BankSafeDocumentMoqData();
            _repositorMoq = new Mock<IBankSafeDocumentRepositorieQuery>();
        }
        [Fact]
        [Trait("Service", "BankSafeDocument")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllBankSafeDocumentsQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllBankSafeDocumentsQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllBankSafeDocumentsQueryHandler bankSafeDocumentService = new GetAllBankSafeDocumentsQueryHandler(
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getAllBankSafeDocumentsQuery = new GetAllBankSafeDocumentsQuery();
            var result = await bankSafeDocumentService.Handle(getAllBankSafeDocumentsQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<BankSafeDocument>>>(result);
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
        [Trait("Service", "BankSafeDocument")]
        [InlineData("3b2667f6-995c-49fb-a36b-195c965f442c")]
        [InlineData("1872255b-72dd-4cc6-84fa-50ab94677aca")]
        [InlineData("17466fd3-e221-413d-a419-dd4690bf7bc1")]
        public async Task GetTestAsync(Guid code)
        {
            Mock<ILogger<GetBankSafeDocumentsQueryHandler>> _loggerMoq = new Mock<ILogger<GetBankSafeDocumentsQueryHandler>>();
            _repositorMoq.Setup(p => p.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetBankSafeDocumentsQueryHandler bankSafeDocumentService = new GetBankSafeDocumentsQueryHandler(
                _repositorMoq.Object,
                _loggerMoq.Object);


            var getBankSafeDocumentsQuery = new GetBankSafeDocumentsQuery()
            {
                Code = code
            };
            var result = await bankSafeDocumentService.Handle(getBankSafeDocumentsQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<BankSafeDocument>>(result);
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
