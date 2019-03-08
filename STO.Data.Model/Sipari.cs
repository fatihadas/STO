using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Data.Model
{
    public class Sipari
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int SiparisId { get; set; }

        public int? UrunId { get; set; }

        public int? MusteriId { get; set; }

        public int? SiparisAdet { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SiparisTarihi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EklenmeTarihi { get; set; }

        public int? SiparisDurum { get; set; }

        public virtual Musteri Musteri { get; set; }

        public virtual SiparisDurum SiparisDurum1 { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
