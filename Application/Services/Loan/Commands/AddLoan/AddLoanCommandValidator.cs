using Domain.Exceptions;
using FluentValidation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Commands.AddLoan
{
    public class AddLoanCommandValidator:AbstractValidator<AddLoanCommand>
    {
        public AddLoanCommandValidator()
        {
            RuleFor(p => p.NameBankSafe)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "NameBankSafe"))
                .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "NameBankSafe", "50"));

            RuleFor(p => p.FirstName)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "FirstName"))
                .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "FirstName", "50"));

            RuleFor(p => p.LastName)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "LastName"))
                .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "LastName", "50"));

            RuleFor(p => p.NumberOfInstallments)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "NumberOfInstallments"));

            RuleFor(p => p.Amount)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Amount"));

            RuleFor(p => p.Wage)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Wage"));

        }
    }
}
