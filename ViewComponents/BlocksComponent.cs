using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;
using Google.Protobuf.WellKnownTypes;
using RecycleCoinBlockExplorer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RecycleCoinBlockExplorer.ViewComponents
{
	public class BlocksComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(Veri veri)
		{
			if (veri.Yukseklik > 0)
				return View("Block", new Veri { Blok = YukseklikIleBlokGetir(veri.Yukseklik), SonBlokYuksekligi = SonBlokYuksekliginiGetir() });
			else if (veri.Karma != null)
				return View("Block", new Veri { Blok = KarmaIleBlokGetir(veri.Karma), SonBlokYuksekligi = SonBlokYuksekliginiGetir() });
			else if (veri.DogrulayiciAdresi != null)
				return View("BlocksList", new Veri { Bloklar = DogrulayiciIleBloklariGetir(veri.DogrulayiciAdresi)});
			return View("BlocksList", new Veri { Bloklar = BloklariGetir()});
		}


		//------ Tekil
		private Blok? KarmaIleBlokGetir(string karma)
		{
			var yanit = Program.blokServis.KarmaIleTekilGetir(new BlokIstek { Karma = karma });
			if (yanit.Durum && yanit.Blok != null) return yanit.Blok;
			else return null;
		}
		
		private Blok? YukseklikIleBlokGetir(long yukseklik)
		{
			var yanit = Program.blokServis.YukseklikIleTekilGetir(new BlokIstek { Yukseklik = yukseklik });
			if (yanit.Durum && yanit.Blok != null) return yanit.Blok;
			else return null;
		}

		private long SonBlokYuksekliginiGetir()
		{
			var yanit = Program.blokServis.SonGetir(new Empty());
			if (yanit != null && yanit.Blok != null)
				return yanit.Blok.Yukseklik;
			else return -1;
		}


		//---- Liste
		public List<Blok>? DogrulayiciIleBloklariGetir(string dogrulayici)
		{
			if (Program.SunucuDurum() && dogrulayici != null)
			{
				var yanit = Program.blokServis.DogrulayiciIleListeGetir(new BlokIstek { Dogrulayici = dogrulayici });
				if (yanit.Durum && yanit.Bloklar != null && yanit.Bloklar.Count > 0) return yanit.Bloklar.ToList();
				else return null;
			}
			return null;
		}

		public List<Blok>? BloklariGetir()
		{
			//do
			//{
			//} while (await Program.sayac.WaitForNextTickAsync());
			if (Program.SunucuDurum())
			{
				var yanit = Program.blokServis.SonNadetBlokGetir(new BlokIstek { Adet = 25 });
				if (yanit != null && yanit.Durum && yanit.Bloklar.Count > 0)
					return yanit.Bloklar.ToList();
			}
			return null;
		}

	}
}
