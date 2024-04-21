using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.Message;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BankSafeService
    {
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BankSafeService> _logger;
        public BankSafeService(IBankSafeRepositorie bankSafeRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<BankSafeService> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> AddAsync(BankSafe bankSafe,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _bankSafeRepositorie.Add(bankSafe);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                    , bankSafe.Name.Value
                    , nameof(AddAsync));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }

        }
        public async Task<OperationResult> UpdateAsync(BankSafe bankSafe,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAsync(bankSafe.Name, cancellationToken);
                if (result != null)
                {
                    result.Update(bankSafe.SharePrice);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , bankSafe.Name.Value
                        , nameof(UpdateAsync));
                    _logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, bankSafe.Name.Value);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult> DeleteAsync(string name,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankSafeRepositorie.DeleteAsync(name, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , name
                        , nameof(DeleteAsync));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<BankSafe>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<List<BankSafe>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafe>>(false, ex.Message, null);
            }

        }
        public async Task<OperationResult<BankSafe>> GetAsync(string name,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAsync(name, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAsync)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<BankSafe>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafe>(false, ex.Message, null);
            }

        }
        public async Task<OperationResult<decimal>> Inventory(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _bankSafeRepositorie.Inventory(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(Inventory)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<decimal>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<decimal>(false, ex.Message, -1);
            }

        }
        
    }
}
