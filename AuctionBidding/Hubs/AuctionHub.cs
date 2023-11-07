using Microsoft.AspNetCore.SignalR;
using AuctionBidding.Models;
using SharedLibrary.Models;
using AuctionBidding.Repositories;

namespace AuctionBidding.Hubs
{
    public class AuctionHub : Hub
    {
        private readonly IAuctionRepo auctionRepo;

        public AuctionHub(IAuctionRepo _auctionRepo)
        {
            auctionRepo = _auctionRepo;
        }
        public async Task NotifyNewBid(AuctionNotify auction)
        {
            //regarding groups

            var groupName = $"auction-{auction.AuctionId}";

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("NotifyOutBid", auction);

            await Clients.All.SendAsync("ReceiveNewBid", auction);
        }
        public async Task NewAuction(Auction auction)
        {
            var newAuction = auctionRepo.AddAuction(auction);
            await Clients.All.SendAsync("ReceiveNewAuction", newAuction);
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
