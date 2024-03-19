using Application.UnitOfWork;
using Domain.IRepositories;
using Application.Models;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

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
                _Logger.LogInformation($"{bankAccount.AccountNumber} is Created Successfully");
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
                    _Logger.LogInformation($"{bankAccount.AccountNumber} is Update Successfully");
                    return new OperationResult(true, null);
                }
                else
                {
                    return new OperationResult(false, "BankAccount Update was not successful");
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
                _Logger.LogInformation($"{accountNumber} is Delete Successfully");
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
                _Logger.LogInformation("GetAll is Successfully");
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
                _Logger.LogInformation("GetAll is Successfully");
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
