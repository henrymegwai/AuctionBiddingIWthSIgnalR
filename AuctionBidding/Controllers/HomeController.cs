using Microsoft.AspNetCore.Mvc;
using AuctionBidding.Models;
using AuctionBidding.Repositories;
using System.Diagnostics;

namespace AuctionBidding.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuctionRepo _AuctionRepo;

        public HomeController(IAuctionRepo auctionRepo)
        {
            _AuctionRepo = auctionRepo;
        }

        public IActionResult Index()
        {
            var auctions = _AuctionRepo.GetAll();
            return View(auctions);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}