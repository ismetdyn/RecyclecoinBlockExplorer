using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;
using RecycleCoinBlockExplorer.Models;
using RecycleCoinBlockExplorer.Models.Entity;

namespace RecycleCoinBlockExplorer.Controllers
{
	[Route("/accounts")]
    public class AccountsController : Controller
    {
		[Route("address={adres}")]
		public IActionResult Index(string adres)
		{
			return View(new Veri { Adres = adres });
		}

		public IActionResult Index()
        {
            return View(new Veri { });
        }
    }
}
