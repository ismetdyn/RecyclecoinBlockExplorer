using Microsoft.AspNetCore.Mvc;
using RecycleCoinBlockExplorer.Models;

namespace RecycleCoinBlockExplorer.Controllers
{
	[Route("/search")]
	public class SearchController : Controller
	{
		[HttpPost]
		public IActionResult Search(string aranacakMetin)
		{
			if (aranacakMetin != null) aranacakMetin = aranacakMetin.Trim();
			if (!string.IsNullOrEmpty(aranacakMetin))
			{
				Veri veri = new Veri();
				var isNumeric = long.TryParse(aranacakMetin, out var num) && num > 0;
				if (isNumeric)
				{
					veri.Yukseklik = long.Parse(aranacakMetin);
					veri.IsSingleBlock= true;
				}
				else if (aranacakMetin.Length > 60)
				{
					veri.Karma = aranacakMetin;
					veri.IsSingleBlock = true;
					veri.IsSingleTransaction = true;
				}
				else 
				{
					veri.Adres = aranacakMetin;
					veri.DogrulayiciAdresi= aranacakMetin;
					veri.IsSingleAccount= true;
					veri.IsSingleBlock = true;
				}
				return View("Index", veri);
			}
			return View("Index", new Veri { });
		}
	}
}
