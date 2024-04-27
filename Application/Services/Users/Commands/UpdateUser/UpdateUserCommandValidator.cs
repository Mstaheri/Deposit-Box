using Application.Services.Users.Commands.AddUser;
using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "FirstName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "FirstName", "50"));

            RuleFor(p => p.LastName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "LastName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "LastName", "50"));

            RuleFor(p => p.PhoneNumber)
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "PhoneNumber", "11"));

            RuleFor(p => p.NationalIDNumber)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "NationalIDNumber"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "NationalIDNumber", "10"));

            RuleFor(p => p.UserName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "UserName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "UserName", "50"));

            RuleFor(p => p.Password)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "Password"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "Password", "50"));
        }
    }
}
