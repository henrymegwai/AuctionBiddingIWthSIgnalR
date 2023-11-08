using AuctionBidding.Hubs;
using AuctionBidding.Repositories;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(o =>
{
    if (!builder.Environment.IsProduction())
    {
        o.EnableDetailedErrors = true;
    }
}).AddMessagePackProtocol()
//Enabling Redis Backplane 
//.AddStackExchangeRedis("redisconnectionstring here");
//Because we are now using Azure SignalR service, there won't be need for using redis anymore
.AddAzureSignalR("azureserviceconnectionstring here")

builder.Services.AddSingleton<IAuctionRepo, AuctionMemoryRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapPost("auction/{auctionId}/newbid", (int auctionId, int currentBid, IAuctionRepo auctionRepo) =>
{
    auctionRepo.NewBid(auctionId, currentBid);
});

//app.MapPost("auction", async (Auction auction, IAuctionRepo auctionRepo, IHubContext<AuctionHub> hubContext) =>
//{
//    auctionRepo.AddAuction(auction);
//    await hubContext.Clients.All.SendAsync("ReceiveNewAuction", auction);
//});

app.MapGet("/auctions", (IAuctionRepo auctionRepo) =>
{
    return auctionRepo.GetAll();
});

app.MapHub<AuctionHub>("/auctionHub");

app.Run();
