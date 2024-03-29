using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.Message;
using Domain.OperationResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BankSafeDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankSafeDocumentRepositorie _bankSafeDocumentRepositorie;
        private readonly ILogger<BankSafeDocumentService> _Logger;
        public BankSafeDocumentService(IUnitOfWork unitOfWork,
            IBankSafeDocumentRepositorie bankSafeDocumentRepositorie,
            ILogger<BankSafeDocumentService> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankSafeDocumentRepositorie = bankSafeDocumentRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> AddAsync(BankSafeDocument bankSafeDocument,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankSafeDocumentRepositorie.AddAsync(bankSafeDocument);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , bankSafeDocument.Code
                        , nameof(AddAsync));
                _Logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<BankSafeDocument>>> GetAllAsync()
        {
            try
            {
                var result = await _bankSafeDocumentRepositorie.GetAllAsync();
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<BankSafeDocument>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafeDocument>>(false, ex.Message, null);
            }
        }
        public async Task<OperationResult<BankSafeDocument>> GetAsync(Guid code)
        {
            try
            {
                var result = await _bankSafeDocumentRepositorie.GetAsync(code);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<BankSafeDocument>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafeDocument>(false, ex.Message, null);
            }
        }
    }
}
