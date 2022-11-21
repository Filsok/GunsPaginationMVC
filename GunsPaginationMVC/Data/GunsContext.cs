using Microsoft.EntityFrameworkCore;
using GunsPaginationMVC.Entities;

namespace GunsPaginationMVC.Data
{
    public class GunsContext : DbContext
    {
        public DbSet<GunsPaginationMVC.Entities.Gun> Gun { get; set; }
        public GunsContext(DbContextOptions<GunsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gun>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Gun>()
                .Property(g => g.Cartridge)
                .IsRequired();
            modelBuilder.Entity<Gun>()
                .Property(g => g.Id)
                .IsRequired();
        }

    }
}