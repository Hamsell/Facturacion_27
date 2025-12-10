using Facturacion_27.Models;
using Microsoft.EntityFrameworkCore;


namespace Facturacion._27.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Factura> Facturas { get; set; } = null!;
        public DbSet<FacturaItem> FacturaItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().Property(c => c.Nombre).IsRequired();
            modelBuilder.Entity<Producto>().Property(p => p.PrecioUnitario).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Factura>().Property(f => f.Subtotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Factura>().Property(f => f.Impuesto).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Factura>().Property(f => f.Total).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FacturaItem>().Property(fi => fi.PrecioUnitario).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FacturaItem>().Property(fi => fi.LineSubtotal).HasColumnType("decimal(18,2)");
        }
    }
}

