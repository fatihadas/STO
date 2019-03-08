using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Data.Model
{

    [Table("Musteri")]
    public class Musteri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Musteri()
        {
            Siparis = new HashSet<Sipari>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [StringLength(50)]
        public string MusteriAdiSoyadi { get; set; }

        [StringLength(10)]
        public string MusteriTel { get; set; }

        public int? MusteriSehirId { get; set; }

        [StringLength(50)]
        public string MusteriAdres { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public virtual Sehir Sehir { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }
    }
}
