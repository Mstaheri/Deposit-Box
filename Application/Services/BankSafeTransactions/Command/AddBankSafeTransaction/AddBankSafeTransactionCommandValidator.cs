using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeTransactions.Command.AddBankSafeTransaction
{
    public class AddBankSafeTransactionCommandValidator
        :AbstractValidator<AddBankSafeTransactionCommand>
    {
        public AddBankSafeTransactionCommandValidator()
        {
            RuleFor(p => p.NameBankSafe)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "NameBankSafe"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "NameBankSafe", "50"));

            RuleFor(p => p.AccountNumber)
           .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "AccountNumber"))
           .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "AccountNumber", "50"));

            RuleFor(p => p.Deposit)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Deposit"));

            RuleFor(p => p.Withdrawal)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Withdrawal"));
        }
    }
}
