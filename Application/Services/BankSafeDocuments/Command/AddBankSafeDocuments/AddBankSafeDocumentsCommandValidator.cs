using Application.UnitOfWork;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeDocuments.Command.AddBankSafeDocuments
{
    public class AddBankSafeDocumentsCommandValidator
        :AbstractValidator<AddBankSafeDocumentsCommand>
    {
        public AddBankSafeDocumentsCommandValidator()
        {
            RuleFor(p => p.NameBankSafe)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "NameBankSafe"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "NameBankSafe", "50"));

            RuleFor(p => p.AccountNumber)
           .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "AccountNumber"))
           .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "AccountNumber", "50"));

            RuleFor(p => p.RegistrationDate)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "RegistrationDate"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "RegistrationDate", "10"));

            RuleFor(p => p.DueDate)
           .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "DueDate"))
           .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "DueDate", "10"));

            RuleFor(p => p.Deposit)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Deposit"));

            RuleFor(p => p.Withdrawal)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Withdrawal"));
        }
    }
}
