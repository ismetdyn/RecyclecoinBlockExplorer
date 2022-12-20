using Microsoft.AspNetCore.Mvc;
using RecycleCoinBlockExplorer.Models;

namespace RecycleCoinBlockExplorer.Controllers
{
	[Route("/pendingtransactions")]
	public class PendingTransactionsController : Controller
	{
		public IActionResult Index()
		{
			return View(new Veri { IsPool = true });
		}

		[Route("hash={karma}")]
		public IActionResult Index(string karma)
		{
			return View(new Veri { IsPool = true, Karma = karma });
		}

		[Route("address={adres}")]
		public IActionResult AdresIleGetir(string adres)
		{
			return View(new Veri {IsPool = true, Adres = adres });
		}
	}
}
