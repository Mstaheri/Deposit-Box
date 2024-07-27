using Application.Services.Loan.Commands.AddLoan;
using Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.AddChatRoom
{
    public class AddChatRoomCommandValidator : AbstractValidator<AddChatRoomCommand>
    {
        public AddChatRoomCommandValidator()
        {
            RuleFor(p => p.ConnectionId)
                .NotNull().WithMessage(string.Format(ConstMessages.IsNull, "ConnectionId"))
                .MaximumLength(23).WithMessage(string.Format(ConstMessages.MaximumLength, "ConnectionId", "23"));
        }
        
    }
}
