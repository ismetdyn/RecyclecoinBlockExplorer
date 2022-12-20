using Microsoft.AspNetCore.Mvc;
using RecycleCoinBlockExplorer.Models;
using RecycleCoinBlockExplorer.ViewComponents;

namespace RecycleCoinBlockExplorer.Controllers
{
	[Route("/blocks")]
	public class BlocksController : Controller
	{

		[Route("height={yukseklik:long}")]
		public IActionResult Index(long yukseklik)
		{
			return View(new Veri { IsSingleBlock = true, Yukseklik = yukseklik });
		}

		[Route("hash={karma}")]
		public IActionResult Index(string karma)
		{
			return View(new Veri { IsSingleBlock = true, Karma = karma });
		}

		public IActionResult Index()
		{
			return View(new Veri { IsTableBlocks = true});
			//return View(new Veri() { IsTableBlocks = true });
		}
	}
}
