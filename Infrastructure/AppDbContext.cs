using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Banco> Bancos => Set<Banco>();
        public DbSet<Boleto> Boletos => Set<Boleto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Banco
            modelBuilder.Entity<Banco>().HasKey(b => b.Id);
            modelBuilder.Entity<Banco>().Property(b => b.Codigo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Banco>().HasIndex(b => b.Codigo).IsUnique();

            // Boleto
            modelBuilder.Entity<Boleto>().HasKey(b => b.Id);
            modelBuilder.Entity<Boleto>()
                .HasOne(b => b.Banco)
                .WithMany(b => b.Boletos)
                .HasForeignKey(b => b.BancoId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
