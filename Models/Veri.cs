using Microsoft.AspNetCore.Mvc;
using RecycleCoin.Grpc;

namespace RecycleCoinBlockExplorer.Models
{
	public class Veri
	{
		public Hesap? Hesap { get; set; } 
		public List<Hesap>? Hesaplar { get; set; }

		public Islem? Islem { get; set; }
		public List<Islem>? Islemler { get; set; }
		public List<Islem>? BekleyenIslemler { get; set; }

		public Blok? Blok { get; set; }
		public List<Blok>? Bloklar { get; set; }

		public long SonBlokYuksekligi { get; set; }
		public string? Karma { get; set; }
		public string? Adres { get; set; }
		public string? DogrulayiciAdresi { get; set; }
		public long Yukseklik { get; set; }	
		public long SonYukseklik { get; set; }
		public double Bakiye { get; set; }
		public double OnaylanmamisBakiye { get; set; }

		public string? Mesaj { get; set; }

		public bool IsSingleTransaction { get; set; }
		public bool IsTableTransactions { get; set; }
		public bool IsSingleBlock { get; set; }
		public bool IsTableBlocks { get; set; }
		public bool IsSingleAccount { get; set; }
		public bool IsTableleAccounts { get; set; }
		public bool IsPool { get; set; }
		public bool IsSuccess { get; set; }
	}
}
