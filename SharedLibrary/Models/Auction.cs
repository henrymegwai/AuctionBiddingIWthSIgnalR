namespace SharedLibrary.Models
{
    public class Auction
    {
        public int Id { get; set; } 
        public string ItemName { get; set; } = string.Empty;
        public int CurrentBid { get; set; }
    }
}
