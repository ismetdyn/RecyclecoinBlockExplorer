using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;
using RecycleCoinBlockExplorer.Models;

namespace RecycleCoinBlockExplorer.ViewComponents
{
	public class TransactionsComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(Veri veri)
		{
			if (veri.IsPool)
			{
				if (veri.Adres != null) return View("TransactionsList", new Veri {
					Islemler = HavuzdanAdresIleIslemleriGetir(veri.Adres), IsPool = true});
				else if (veri.Karma != null) return View("Transaction", new Veri {
					Islem = HavuzdanKarmaIleIslemGetir(veri.Karma), IsPool = true});
				else return View("TransactionsList", new Veri { 
					Islemler = HavuzanIslemleriGetir(), IsPool = true });
			}
			else
			{
				if (veri.Adres != null) return View("TransactionsList", new Veri { Islemler = AdresIleIslemleriGetir(veri.Adres) });
				else if (veri.Yukseklik > 0) return View("TransactionsList", new Veri { Islemler = YukseklikIleIslemleriGetir(veri.Yukseklik) });
				else if (veri.Karma != null) return View("Transaction", new Veri { Islem = KarmaIleIslemGetir(veri.Karma) });
				else return View("TransactionsList", new Veri { Islemler = IslemleriGetir() });
			}
			
		}

		#region Islemler

		// ---- Liste
		public List<Islem>? AdresIleIslemleriGetir(string adres)
		{
			if (Program.SunucuDurum() && adres != null)
			{
				var yanit = Program.islemServis.AdresIleListeGetir(new IslemIstek { Adres = adres });
				if (yanit.Durum && yanit.Islemler.Count > 0) { return yanit.Islemler.ToList(); }
				else return null;
			}
			else return null;
		}

		public List<Islem>? YukseklikIleIslemleriGetir(long yukseklik = -1)
		{
			if (Program.SunucuDurum() && yukseklik > 0)
			{
				var yanit = Program.islemServis.YukseklikIleIslemListeGetir(new IslemIstek { Yukseklik = yukseklik });
				if (yanit.Durum && yanit.Islemler.Count > 0) { return yanit.Islemler.ToList(); }
				else return null;
			}
			else return null;
		}

		public List<Islem>? IslemleriGetir()
		{
			if (Program.SunucuDurum())
			{
				var yanit = Program.islemServis.SonNadetIslemGetir(new IslemIstek { Adet = 25 });
				if (yanit.Durum && yanit.Islemler.Count > 0) return yanit.Islemler.ToList();
				else return null;
			}
			else return null;
		}

		//--- Tekil
		public Islem? KarmaIleIslemGetir(string karma = null)
		{
			if (karma != null)
			{
				var yanit = Program.islemServis.KarmaIleTekilGetir(new IslemIstek { Karma = karma });
				if (yanit.Durum && yanit.Islem != null) { return yanit.Islem; }
				else return null;
			}
			else return null;
		}
		#endregion

		#region HavuzIslemleri
		public List<Islem>? HavuzdanAdresIleIslemleriGetir(string adres)
		{
			if (Program.SunucuDurum() && adres != null)
			{
				var yanit = Program.islemServis.HavuzdanAdresIleListeGetir(new IslemIstek { Adres = adres });
				if (yanit.Durum && yanit.Islemler.Count > 0) { return yanit.Islemler.ToList(); }
				else return null;
			}
			else return null;
		}

		public List<Islem>? HavuzanIslemleriGetir()
		{
			if (Program.SunucuDurum())
			{
				var yanit = Program.islemServis.HavuzdanSonNadetIslemGetir(new IslemIstek { Adet = 25 });
				if (yanit.Durum && yanit.Islemler.Count > 0) return yanit.Islemler.ToList();
				else return null;
			}
			else return null;
		}

		//--- Tekil
		public Islem? HavuzdanKarmaIleIslemGetir(string karma = null)
		{
			if (karma != null)
			{
				var yanit = Program.islemServis.HavuzdanKarmaIleTekilGetir(new IslemIstek { Karma = karma });
				if (yanit.Durum && yanit.Islem != null) { return yanit.Islem; }
				else return null;
			}
			else return null;
		}
		#endregion
	}
}
