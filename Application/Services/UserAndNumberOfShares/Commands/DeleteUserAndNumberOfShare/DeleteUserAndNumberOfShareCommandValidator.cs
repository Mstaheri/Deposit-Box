﻿using Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare;
using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAndNumberOfShares.Commands.DeleteUserAndNumberOfShare
{
    public class DeleteUserAndNumberOfShareCommandValidator
        : AbstractValidator<DeleteUserAndNumberOfShareCommand>
    {
        public DeleteUserAndNumberOfShareCommandValidator()
        {
            RuleFor(p => p.NameBankSafe)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "NameBankSafe"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "NameBankSafe", "50"));

            RuleFor(p => p.UserName)
            .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "UserName"))
            .MaximumLength(50).WithMessage(string.Format(ConstMessages.MaximumLength, "UserName", "50"));
        }
    }
}
