using Application.IRepositories;
using Application.Models;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<OperationResult> Add(BankAccount bankAccount ,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _bankAccountRepositorie.AddAsync(bankAccount);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _Logger.LogInformation($"{bankAccount.AccountNumber} is Created Successfully");
                return new OperationResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
