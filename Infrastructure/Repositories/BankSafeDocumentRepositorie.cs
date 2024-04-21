using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.Message;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BankSafeDocumentRepositorie : IBankSafeDocumentRepositorie
    {
        private readonly DbSet<BankSafeDocument> _bankSafeDocument;
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        public BankSafeDocumentRepositorie(IUnitOfWork unitOfWork,
            IBankSafeRepositorie bankSafeRepositorie,
            IBankAccountRepositorie bankAccountRepositorie)
        {
            _bankSafeDocument = unitOfWork.Set<BankSafeDocument>();
            _bankSafeRepositorie = bankSafeRepositorie;
            _bankAccountRepositorie = bankAccountRepositorie;
        }
        public async ValueTask AddAsync(BankSafeDocument bankSafeDocument ,CancellationToken cancellationToken)
        {
            var resultbankAccount = await _bankAccountRepositorie
                .GetAsync(bankSafeDocument.AccountNumber , cancellationToken);
            if (resultbankAccount != null)
            {
                var resultBankSafe = await _bankSafeRepositorie
                    .GetAsync(bankSafeDocument.NameBankSafe, cancellationToken);
                if (resultBankSafe != null)
                {
                    if (bankSafeDocument.Withdrawal.Value != 0)
                    {
                        var inventory = await _bankSafeRepositorie
                            .InventoryBankAccount(bankSafeDocument.AccountNumber,
                            bankSafeDocument.NameBankSafe , cancellationToken);

                        if (inventory < bankSafeDocument.Withdrawal.Value)
                        {
                            string message = string.Format(ConstMessages.NotInventory
                                , bankSafeDocument.AccountNumber.Value
                                , bankSafeDocument.NameBankSafe.Value
                                , inventory);
                            throw new Exception(message);
                        }
                    }
                     _bankSafeDocument.Add(bankSafeDocument);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, bankSafeDocument.NameBankSafe.Value);
                    throw new Exception(message);
                }
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, bankSafeDocument.AccountNumber.Value);
                throw new Exception(message);
            }
        }

        public async Task<List<BankSafeDocument>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankSafeDocument.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankSafeDocument> GetAsync(Guid code , CancellationToken cancellationToken)
        {
            var result = await _bankSafeDocument.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
            return result;
        }
    }
}
