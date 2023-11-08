// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Models;
using System.Net.Http.Json;


using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("http://localhost:5247");
var response = await httpClient.GetAsync("/auctions");
var auctions = await response.Content.ReadFromJsonAsync<Auction[]>();

if (auctions is null)
    return;

foreach (var auction in auctions)
{
   Console.WriteLine($"{auction.Id,-3} {auction.ItemName,-20} " + $"{auction.CurrentBid,10}");
}


var connection = new HubConnectionBuilder().WithUrl("http://localhost:5247/auctionhub", option =>
{
    option.Transports = HttpTransportType.WebSockets;
    option.SkipNegotiation = true;

}).AddMessagePackProtocol().Build();


connection.On("ReceiveNewBid", (AuctionNotify auctionNotify) => {
    var auction = auctions.Single(a => a.Id == auctionNotify.AuctionId);
    auction.CurrentBid = auctionNotify.NewBid;
    Console.WriteLine("New bid:");
    Console.WriteLine($"{auction.Id,-3} {auction.ItemName,-20} " + $"{auction.CurrentBid,10}");
});

try
{
    await connection.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    while (true)
    {
        Console.WriteLine("Auction id?");
        var id = Console.ReadLine();
        Console.WriteLine("New bid for auction {id}?");
        var bid = Console.ReadLine();
        await connection.InvokeAsync("NotifyNewBid", new AuctionNotify { AuctionId = int.Parse(id!), NewBid = int.Parse(bid!) });
        Console.WriteLine("Bid placed");
    }
}
finally
{

    await connection.StopAsync();
}