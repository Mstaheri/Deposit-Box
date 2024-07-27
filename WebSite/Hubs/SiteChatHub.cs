using Application.Services.ChatRoom.Commands.AddChatRoom;
using Application.Services.ChatRoom.Queries.GetChatRoomByConnectionId;
using Glimpse.Core.ClientScript;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using WebSite.Controllers;

namespace WebSite.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IMediator _mediator;

        public SiteChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendNewMessage(string Sender, string Message)
        {
            var query = new GetChatRoomByConnectionIdQuery
            { ConnectionId = Context.ConnectionId };
            var roomId = await _mediator.Send(query);
            await Clients.Groups(roomId.ToString()).SendAsync("getNewMessage", Sender, Message, DateTime.Now.ToShortTimeString());
            
        }


        public override async Task OnConnectedAsync()
        {
            var query = new AddChatRoomCommand
            { ConnectionId = Context.ConnectionId };
            var roomId = await _mediator.Send(query);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.Data.ToString());
            await Clients.Caller
                .SendAsync("getNewMessage"
                , "پشتیبانی سایت"
                , "سلام وقت بخیر چطوری میتونم کمکتون کنم ؟"
                , DateTime.Now.ToShortTimeString());

            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
