using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankAccounts.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommandValidator : AbstractValidator<DeleteBankAccountCommand>
    {
        public DeleteBankAccountCommandValidator()
        {
            RuleFor(p => p.AccountNumber)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "AccountNumber"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "AccountNumber", "16"));
        }
    }
}
