using AuctionBidding.Models;

namespace AuctionBidding.Repositories
{
    public interface IAuctionRepo
    {
        IEnumerable<Auction> GetAll();
        void NewBid(int auctionId, int newBid);
    }
}