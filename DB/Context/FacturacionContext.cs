using Microsoft.EntityFrameworkCore;
using DB.Models;

namespace DB.Context
{
    public class FacturacionContext : DbContext
    {
        public FacturacionContext(DbContextOptions<FacturacionContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
