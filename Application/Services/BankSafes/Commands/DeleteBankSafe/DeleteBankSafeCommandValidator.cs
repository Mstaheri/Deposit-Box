using Application.Services.BankAccounts.Commands.AddBankAccount;
using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Commands.DeleteBankSafe
{
    public class DeleteBankSafeCommandValidator : AbstractValidator<DeleteBankSafeCommand>
    {
        public DeleteBankSafeCommandValidator()
        {
            RuleFor(p => p.Name)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Name"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "Name", "50"));

        }
    }
}
