using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.OperationResults;
using Domain.Message;
using Domain.ValueObjects;

namespace Application.Services
{
    public class BankAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        private readonly ILogger<BankAccountService> _Logger;
        public BankAccountService(IUnitOfWork unitOfWork,
            IBankAccountRepositorie bankAccountRepositorie,
            ILogger<BankAccountService> Logger)
            
            {
            _unitOfWork = unitOfWork;
            _bankAccountRepositorie = bankAccountRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> AddAsync(BankAccount bankAccount ,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankAccountRepositorie.AddAsync(bankAccount);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , bankAccount.AccountNumber.Value
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
        public async Task<OperationResult> UpdateAsync(BankAccount bankAccount,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _bankAccountRepositorie.GetAsync(bankAccount.AccountNumber);
                if (result != null)
                {
                    result.Update(bankAccount.AccountName, bankAccount.BankName, bankAccount.Description);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , bankAccount.AccountNumber.Value
                        , nameof(UpdateAsync));
                    _Logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, bankAccount.AccountNumber.Value);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            } 
        }
        public async Task<OperationResult> DeleteAsync(string accountNumber,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankAccountRepositorie.DeleteAsync(accountNumber);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , accountNumber
                        , nameof(DeleteAsync));
                _Logger.LogInformation(message);
                return new OperationResult(true,null);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex , ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<BankAccount>>> GetAllAsync()
        {
            try
            {
                var result = await _bankAccountRepositorie.GetAllAsync();
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<BankAccount>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankAccount>>(false, ex.Message, null);
            }
        }
        public async Task<OperationResult<BankAccount>> GetAsync(string accountNumber)
        {
            try
            {
                var result = await _bankAccountRepositorie.GetAsync(accountNumber);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<BankAccount>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<BankAccount>(false, ex.Message, null);
            }
        }
    }
}
