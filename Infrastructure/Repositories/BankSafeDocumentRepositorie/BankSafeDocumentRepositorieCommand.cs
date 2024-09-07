using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using Domain.IRepositories.IBankSafeRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankSafeDocumentRepositorie
{
    public class BankSafeDocumentRepositorieCommand : IBankSafeDocumentRepositorieCommand
    {
        private readonly DbSet<BankSafeDocument> _bankSafeDocument;
        private readonly IBankSafeRepositorieQuery _bankSafeRepositorie;
        private readonly IBankAccountRepositorieQuery _bankAccountRepositorie;
        public BankSafeDocumentRepositorieCommand(IUnitOfWork unitOfWork,
            IBankSafeRepositorieQuery bankSafeRepositorie,
            IBankAccountRepositorieQuery bankAccountRepositorie)
        {
            _bankSafeDocument = unitOfWork.Set<BankSafeDocument>();
            _bankSafeRepositorie = bankSafeRepositorie;
            _bankAccountRepositorie = bankAccountRepositorie;
        }
        public async ValueTask AddAsync(BankSafeDocument bankSafeDocument, CancellationToken cancellationToken)
        {
            var resultbankAccount = await _bankAccountRepositorie
                .GetAsync(bankSafeDocument.AccountNumber, cancellationToken);
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
                            bankSafeDocument.NameBankSafe, cancellationToken);

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
    }
}
