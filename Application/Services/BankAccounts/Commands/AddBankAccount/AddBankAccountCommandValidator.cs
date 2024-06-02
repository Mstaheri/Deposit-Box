using Application.UnitOfWork;
using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankAccounts.Commands.AddBankAccount
{
    public class AddBankAccountCommandValidator : AbstractValidator<AddBankAccountCommand>
    {
        public AddBankAccountCommandValidator()
        {
             RuleFor(p => p.AccountNumber)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "AccountNumber"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "AccountNumber", "16"));

            RuleFor(p => p.UserName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "UserName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "UserName", "50"));

            RuleFor(p => p.AccountName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "AccountName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "AccountName", "50"));

            RuleFor(p => p.BankName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "BankName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "BankName", "50"));

            RuleFor(p => p.Description)
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "Password", "500"));
        }
    }
}
