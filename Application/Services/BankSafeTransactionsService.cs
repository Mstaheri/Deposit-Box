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
    public class BankSafeTransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankSafeTransactionsRepositorie _bankSafeTransactionsRepositorie;
        private readonly ILogger<BankSafeTransactionsService> _Logger;
        public BankSafeTransactionsService(IUnitOfWork unitOfWork,
            IBankSafeTransactionsRepositorie bankSafeTransactionsRepositorie,
            ILogger<BankSafeTransactionsService> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankSafeTransactionsRepositorie = bankSafeTransactionsRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> AddAsync(BankSafeTransactions bankSafeRepositorie,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankSafeTransactionsRepositorie.AddAsync(bankSafeRepositorie);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , bankSafeRepositorie.Code
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
        public async Task<OperationResult<List<BankSafeTransactions>>> GetAllAsync()
        {
            try
            {
                var result = await _bankSafeTransactionsRepositorie.GetAllAsync();
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<BankSafeTransactions>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafeTransactions>>(false, ex.Message, null);
            }
        }
        public async Task<OperationResult<BankSafeTransactions>> GetAsync(Guid code)
        {
            try
            {
                var result = await _bankSafeTransactionsRepositorie.GetAsync(code);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<BankSafeTransactions>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafeTransactions>(false, ex.Message, null);
            }
        }
    }
}
