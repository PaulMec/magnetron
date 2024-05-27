using DB.Models;
using DB.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DB.Context
{
    public class FacturacionContext : DbContext
    {
        public FacturacionContext(DbContextOptions<FacturacionContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        // DbSet para las vistas SQL
        public DbSet<ProductSoldViewModel> ProductSoldViewModels { get; set; }
        public DbSet<ProductProfitViewModel> ProductProfitViewModels { get; set; }
        public DbSet<ProductProfitMarginViewModel> ProductProfitMarginViewModels { get; set; }
        public DbSet<PersonTotalBilledViewModel> PersonTotalBilledViewModels { get; set; }
        public DbSet<PersonMostExpensiveProductViewModel> PersonMostExpensiveProductViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de las vistas SQL como entidades no mapeadas
            modelBuilder.Entity<ProductSoldViewModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<ProductProfitViewModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<ProductProfitMarginViewModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<PersonTotalBilledViewModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<PersonMostExpensiveProductViewModel>().HasNoKey().ToView(null);

            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<InvoiceHeader>().ToTable("InvoiceHeader");
            modelBuilder.Entity<InvoiceDetail>().ToTable("InvoiceDetail");

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Invoices)
                .WithOne(f => f.Person)
                .HasForeignKey(f => f.PersonId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.InvoiceDetails)
                .WithOne(f => f.Product)
                .HasForeignKey(f => f.ProductId);

            modelBuilder.Entity<InvoiceHeader>()
                .HasMany(f => f.InvoiceDetails)
                .WithOne(d => d.InvoiceHeader)
                .HasForeignKey(d => d.InvoiceHeaderId);
        }
    }
}
