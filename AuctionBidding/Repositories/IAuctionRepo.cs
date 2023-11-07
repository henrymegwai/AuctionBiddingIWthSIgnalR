using SharedLibrary.Models;

namespace AuctionBidding.Repositories
{
    public interface IAuctionRepo
    {
        Auction AddAuction(Auction auction);
        IEnumerable<Auction> GetAll();
        void NewBid(int auctionId, int newBid);
    }
}