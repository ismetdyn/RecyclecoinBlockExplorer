using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;
using RecycleCoinBlockExplorer.Models;

namespace RecycleCoinBlockExplorer.Controllers
{
	public class PaymentController : Controller
	{
		[HttpPost]
		public IActionResult Index(string adres)
		{
			if (!Program.SunucuDurum())
			{
				return View(new Veri { 
					Mesaj = "Sunucuya bağlanılamıyor\\n,\" +\r\n\t\t\t\t\t\"bağlantı sağlandıktan sonra" +
					" lütfen tekrar deneyin.",});
			}
			if (adres != null) adres = adres.Trim();
			if (string.IsNullOrEmpty(adres) || adres.Length < 30)
			{
				return View(new Veri { Mesaj = "Lutfen Geçerli bir adres giriniz",});
			}
			double tutar = 50;
			bool sonuc = GonderimYap(adres, tutar);
			if (sonuc)
			{
				return View(new Veri
				{
					Mesaj = $"{adres[..10]}... adresine {tutar} RC gönderilmek üzere işlem havuzuna eklenmiştir. ",
					Adres = adres,
					IsPool = true,
					IsSuccess = true
				});
			}

			return View(new Veri
			{
				Mesaj = "Gönderme işlemi başarısız",
				Adres = adres,
			});
		}

		public IActionResult Index()
		{
			return View(new Veri { });
		}

		private bool GonderimYap(string aliciAdres, double tutar)
		{
			bool sonuc = false;
			if (Program.SunucuDurum())
			{
				Islem islem;
				try
				{
					var aktarimYaniti = Program.islemServis.ExplorerdanAl(
						new IslemIstek
						{
							Islem = new Islem
							{
								Alici = aliciAdres,
								Tutar = tutar,
							}
						});
					if (aktarimYaniti != null && aktarimYaniti.Durum == true) sonuc = true;
					else return sonuc = false;
				}
				catch
				{
					throw;
				}
			}
			return sonuc;
		}
	}
}
