using STO.Data.Model;
using System.Data.Entity;

namespace STO.Data.Context
{
    public class SiparisContext : DbContext
    {
        public SiparisContext()
            : base("name=SiparisContext")
        {
        }

        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<Musteri> Musteris { get; set; }
        public virtual DbSet<Sehir> Sehirs { get; set; }
        public virtual DbSet<Sipari> Siparis { get; set; }
        public virtual DbSet<SiparisDurum> SiparisDurums { get; set; }
        public virtual DbSet<Urun> Uruns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sehir>()
                .HasMany(e => e.Musteris)
                .WithOptional(e => e.Sehir)
                .HasForeignKey(e => e.MusteriSehirId);

            modelBuilder.Entity<SiparisDurum>()
                .HasMany(e => e.Siparis)
                .WithOptional(e => e.SiparisDurum1)
                .HasForeignKey(e => e.SiparisDurum);

            modelBuilder.Entity<Urun>()
                .Property(e => e.UrunFiyat)
                .HasPrecision(10, 2);
        }
    }
}
