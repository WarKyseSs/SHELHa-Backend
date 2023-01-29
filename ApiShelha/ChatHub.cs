using Infrastructure.Ef;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApiShelha;

[Authorize]
public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
        return base.OnConnectedAsync();
    }
    
    
    //send a message to all users
    public async Task SendMessage ( String user, String message )
    {
        await Clients.All.SendAsync( "SendMessage" , user, message );
    }
    
    //send un message to a specific user
    public async Task SendPrivateMessage(string recipientId, string message)
    {
        await Clients.User(recipientId).SendAsync("SendPrivateMessage", message);
    }
    
    //send un message to a specific user
    public Task SendMessageToGroup(string receiver, string message, int idConversation)
    {
        return Clients.Group(receiver).SendAsync("SendMessageToGroup"
            , Context.User.Identity.Name, message,idConversation);
    }

    //disconnect from conversation
    public override Task OnDisconnectedAsync(Exception? exception)
    {

        Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
        return base.OnDisconnectedAsync(exception);
    }
}