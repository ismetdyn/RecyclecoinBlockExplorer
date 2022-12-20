using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;
using RecycleCoinBlockExplorer.Models;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;

namespace RecycleCoinBlockExplorer.Controllers
{
	public class HomeController : Controller
	{
		long _sonBlokYuksekligi;
		long _islemSayisi = 0;
		double _toplamIslemTutari = 0;
		long _havuzdakiIslemSayisi = 0;
		double _havuzdakiToplamIslemTutari = 0;
		long _hesapSayisi = 0;
		double _hesaplardakiToplamPara = 0;
		List<Blok> _bloklar;
		List<Hesap> _hesaplar;
		List<Islem> _islemler;
		List<Islem> _islemHavuzu;

		public HomeController()
		{
			_bloklar = new List<Blok>();
			_islemler = new List<Islem>();
			_islemHavuzu = new List<Islem>();
			_hesaplar = new List<Hesap>();
			Program.SunucuDurum();
			Guncelle();
		}

		public void AgGenelBilgisiGuncelle()
		{
			if (Program.SunucuDurum())
			{
				var yanit = Program.genelBilgiServisi.AgGenelBilgisiGetir(new Empty());
				if (yanit.Durum)
				{
					_sonBlokYuksekligi = yanit.SonBlokYuksekligi;
					_islemSayisi = yanit.IslemSayisi;
					_toplamIslemTutari = yanit.ToplamIslemTutari;
					_havuzdakiIslemSayisi = yanit.HavuzdakiIslemSayisi;
					_havuzdakiToplamIslemTutari = yanit.HavuzdakiToplamIslemTutari;
					_hesapSayisi = yanit.ToplamHesapSayisi;
					_hesaplardakiToplamPara = yanit.HesaplardakiToplamPara;
				}
			}
		}

		public void WidgetGuncelle()
		{
			if (Program.SunucuDurum())
			{
				var yanit = Program.blokServis.SonNadetBlokGetir(new BlokIstek { Adet = 5 });
				if (yanit != null && yanit.Durum && yanit.Bloklar.Count > 0)
				{
					_bloklar.Clear();
					_bloklar.AddRange(yanit.Bloklar);
				}

				var yanit2 = Program.islemServis.SonNadetIslemGetir(new IslemIstek { Adet = 5 });
				if (yanit2 != null && yanit2.Durum && yanit2.Islemler.Count > 0)
				{
					_islemler.Clear();
					_islemler.AddRange(yanit2.Islemler);
				}

				var yanit3 = Program.islemServis.HavuzdanSonNadetIslemGetir(new IslemIstek { Adet = 5 });
				if (yanit3 != null && yanit3.Durum && yanit3.Islemler.Count > 0)
				{
					_islemHavuzu.Clear();
					_islemHavuzu.AddRange(yanit3.Islemler);
				}
			}
		}

		public async void Guncelle()
		{
			//do
			//{
			//} while (await Program.sayac.WaitForNextTickAsync());
			try
			{

				AgGenelBilgisiGuncelle();
				WidgetGuncelle();

			}
			catch (Exception)
			{
				throw;
			}
		}

		public IActionResult Index()
		{
			ViewBag.SonBlokYuksekligi = _sonBlokYuksekligi;
			ViewBag.IslemSayisi = _islemSayisi;
			ViewBag.ToplamIslemTutari = _toplamIslemTutari;
			ViewBag.HavuzdakiIslemSayisi = _havuzdakiIslemSayisi;
			ViewBag.HavuzdakiToplamIslemTutari = _havuzdakiToplamIslemTutari;
			ViewBag.HesapSayisi = _hesapSayisi;
			ViewBag.HesaplardakiToplamPara = _hesaplardakiToplamPara;
			ViewBag.Bloklar = _bloklar;
			ViewBag.Islemler = _islemler;
			ViewBag.IslemHavuzu = _islemHavuzu;
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}