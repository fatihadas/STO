using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Data.Model
{
    [Table("Kullanici")]
    public class Kullanici
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
        public int KullaniciId { get; set; }

        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [StringLength(50)]
        public string KullaniciSifre { get; set; }

        [StringLength(50)]
        public string SonGirisTarihi { get; set; }

        [StringLength(50)]
        public string KullaniciAdiSoyadi { get; set; }

        [StringLength(50)]
        public string KullaniciEmail { get; set; }

        [StringLength(150)]
        public string KullaniciResimUrl { get; set; }
    }
}
