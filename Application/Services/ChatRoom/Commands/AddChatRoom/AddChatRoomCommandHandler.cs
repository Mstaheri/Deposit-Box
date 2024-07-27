using Application.Services.Loan.Commands.AddLoan;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Glimpse.Core.Extensibility;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.AddChatRoom
{
    public class AddChatRoomCommandHandler
        : IRequestHandler<AddChatRoomCommand, OperationResult<Guid>>
    {
        public AddChatRoomCommandHandler(IUnitOfWork unitOfWork,
            IChatRoomRepositorie chatRoomRepositorie,
            ILogger<AddChatRoomCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _chatRoomRepositorie = chatRoomRepositorie;
            _logger = logger;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatRoomRepositorie _chatRoomRepositorie;
        private readonly ILogger<AddChatRoomCommandHandler> _logger;
        public async Task<OperationResult<Guid>> Handle(AddChatRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chatRomm = new Domain.Entity.ChatRoom(request.ConnectionId);
                await _chatRoomRepositorie.AddAsync(chatRomm);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                string message = string.Format(ConstMessages.Successfully
                , chatRomm.ConnectionId
                , nameof(AddChatRoomCommandHandler));
                _logger.LogInformation(message);
                return new OperationResult<Guid>(true, null , chatRomm.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<Guid>(false, ex.Message , Guid.Empty);
            }

        }
    }
}
