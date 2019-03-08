using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Data.Model
{
    [Table("Urun")]
    public class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            Siparis = new HashSet<Sipari>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [StringLength(50)]
        public string UrunAdi { get; set; }

        [StringLength(50)]
        public string UrunBarkod { get; set; }

        public decimal? UrunFiyat { get; set; }

        [StringLength(50)]
        public string UrunAciklama { get; set; }

        public int? UrunAdet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }
    }
}
