using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Data.Model
{
    [Table("Sehir")]
    public partial class Sehir
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sehir()
        {
            Musteris = new HashSet<Musteri>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int SehirId { get; set; }

        [StringLength(50)]
        public string SehirAdi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Musteri> Musteris { get; set; }
    }
}
