using AuctionBidding.Hubs;
using AuctionBidding.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
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

app.MapHub<AuctionHub>("/auctionhub");

app.Run();
