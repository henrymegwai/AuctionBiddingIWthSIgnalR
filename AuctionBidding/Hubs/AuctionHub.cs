using Microsoft.AspNetCore.SignalR;
using AuctionBidding.Models;
using SharedLibrary.Models;

namespace AuctionBidding.Hubs
{
    public class AuctionHub: Hub
    {
        public async Task NotifyNewBid(AuctionNotify auction)
        {
            //regarding groups

            var groupName = $"auction-{auction.AuctionId}";

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("NotifyOutBid", auction);

            await Clients.All.SendAsync("ReceiveNewBid", auction);
        }

        //public override Task OnConnectedAsync()
        //{
        //    if (Context.User != null)
        //    {
        //        bool isAuthenticated = Context.User.Identity.IsAuthenticated;
        //    }
        //    //we can also check for the role the user has in the claims
        //    //Context.User.Claims


        //    //also with connectionId we could place users in certain groups as soon as they connect and remove them if needed in OnDisconnectedAsync

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return base.OnDisconnectedAsync(exception); 
        //}
    }
}
