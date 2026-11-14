using System;
using System.Collections.Generic;
using System.Text;

namespace GumrukTakipSistemi
{
    public enum IsTipi
    {
        Ihracat,
        Ithalat,
        Redrese,
        BeyannameIptali
    }

    public enum Durum
    {
        Bekliyor,
        DevamEdiyor,
        Tamamlandi
    }

    public class IsTakip
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public IsTipi IsTipi { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime Tarih { get; set; }
        public Durum Durum { get; set; }
    }
}
