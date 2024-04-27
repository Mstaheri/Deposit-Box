using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(p => p.UserName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "UserName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "UserName", "50"));
        }
    }
}
