using Microsoft.AspNetCore.Mvc;
using RecycleCoinBlockExplorer.Models;

namespace RecycleCoinBlockExplorer.Controllers
{
	[Route("/transactions")]
	public class TransactionsController : Controller
	{

		[Route("height={yukseklik:long}")]
		public IActionResult Index(long yukseklik)
		{
			return View(new Veri {Yukseklik = yukseklik });
		}

		[Route("hash={karma}")]
		public IActionResult Index(string karma)
		{
			return View(new Veri {Karma = karma });
		}

		[Route("address={adres}")]
		public IActionResult AdresIleGetir(string adres) 
		{
			return View(new Veri {Adres = adres });
		}

		public IActionResult Index()
		{
			return View(new Veri { });
		}
	}
}
