using SharedLibrary.Models;

namespace AuctionBidding.Repositories
{
    public class AuctionMemoryRepo : IAuctionRepo
    {
        private readonly List<Auction> auctions = new List<Auction>();

        public AuctionMemoryRepo()
        {
            auctions.Add(new Auction { Id = 1, ItemName = "Honda Civic", CurrentBid = 2336 });
            auctions.Add(new Auction { Id = 2, ItemName = "Mercedes Benz", CurrentBid = 40021 });
            auctions.Add(new Auction { Id = 3, ItemName = "Ford", CurrentBid = 1433 });
            auctions.Add(new Auction { Id = 4, ItemName = "Toyota Sienna", CurrentBid = 1283 });
            auctions.Add(new Auction { Id = 5, ItemName = "Tesla", CurrentBid = 39000 });
        }

        public IEnumerable<Auction> GetAll()
        {
            return auctions;
        }

        public void NewBid(int auctionId, int newBid)
        {
            var auction = auctions.Single(a => a.Id == auctionId);
            auction.CurrentBid = newBid;
        }
    }
}
