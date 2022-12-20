using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;
using RecycleCoinBlockExplorer.Models;

namespace RecycleCoinBlockExplorer.ViewComponents
{
	public class AccountsComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(Veri veri)
		{	if (veri.Adres != null)
			{
				return View("Account", new Veri { 
					Hesap = HesapGetir(veri.Adres), 
					OnaylanmamisBakiye = OnaylanmamisBakiyeHesapla(veri.Adres)
				});
			}
			return View("AccountsList", new Veri { Hesaplar = HesaplariGetir()});
		}

		private Hesap? HesapGetir(string adres)
		{
			if (Program.SunucuDurum())
			{
				var yanit = Program.hesapServis.AdresIleGetir(new HesapIstek { Adres = adres });
				if (!yanit.Durum || yanit.Hesap == null) return null;
				else return yanit.Hesap;
			}
			else return null;
		}

		public double OnaylanmamisBakiyeHesapla(string adres)
		{
			double onaylanmamisBakiye = 0.0;
			if(Program.SunucuDurum())
			{
				var yanit = Program.islemServis.HavuzdanAdresIleListeGetir(new IslemIstek { Adres = adres});
				if(yanit != null && yanit.Durum && yanit.Islemler != null && yanit.Islemler.Count > 0)
				{
					foreach (var islem in yanit.Islemler)
					{
						if (islem.Gonderen == adres) onaylanmamisBakiye -= islem.Tutar - islem.Ucret;
						else if (islem.Alici == adres) onaylanmamisBakiye += islem.Tutar;
					}
					return onaylanmamisBakiye;
				}
			}
			return onaylanmamisBakiye;
		}


		//---- List
		public List<Hesap>? HesaplariGetir()
		{
			if (Program.SunucuDurum())
			{
				var yanit = Program.hesapServis.EnZenginNadetHesapGetir(new HesapIstek { Adet = 25 });
				if (yanit != null && yanit.Durum && yanit.Hesaplar.Count > 0)
					return yanit.Hesaplar.ToList();
				else return null;
			}
			return null;
		}
	}
}
