using Application.Services.Loan.Queries.GetAllLoan;
using Application.Services.Loan.Queries.GetByCodeLoan;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IChatRoomRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Queries.GetChatRoomByConnectionId
{
    public class GetChatRoomByConnectionIdQueryHandler :
        IRequestHandler<GetChatRoomByConnectionIdQuery, Guid>
    {
        private readonly IChatRoomRepositorieQuery _chatRoomRepositorie;
        private readonly ILogger<GetChatRoomByConnectionIdQueryHandler> _logger;
        public GetChatRoomByConnectionIdQueryHandler(IChatRoomRepositorieQuery chatRoomRepositorie
            , ILogger<GetChatRoomByConnectionIdQueryHandler> logger)
        {
            _chatRoomRepositorie = chatRoomRepositorie;
            _logger = logger;
        }
        public async Task<Guid> Handle(GetChatRoomByConnectionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _chatRoomRepositorie.GetChatRoomByConnectionId
                    (request.ConnectionId, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetChatRoomByConnectionIdQueryHandler)
                        , "");
                _logger.LogInformation(message);
                if (result != null)
                {
                    return result.Id;
                }
                else
                {
                    return Guid.Empty;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Guid.Empty;
            }
        }
    }
}
