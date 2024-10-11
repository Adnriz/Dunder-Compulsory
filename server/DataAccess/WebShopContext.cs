using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public partial class WebShopContext : DbContext
    {
        public WebShopContext(DbContextOptions<WebShopContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PaperProperty> PaperProperties { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderEntry> OrderEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("customers_pkey");
                entity.ToTable("customers");
                entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("customers_email_key");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(255);
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(50);
                entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.HasMany(e => e.Orders).WithOne(o => o.Customer).HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Paper
            modelBuilder.Entity<Paper>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("paper_pkey");
                entity.ToTable("paper");
                entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("unique_product_name");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Discontinued).HasColumnName("discontinued").IsRequired();
                entity.Property(e => e.Stock).HasColumnName("stock").IsRequired();
                entity.Property(e => e.Price).HasColumnName("price").IsRequired();
            });

            // Configure Property
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("properties_pkey");
                entity.ToTable("properties");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.PropertyName).HasColumnName("property_name").IsRequired().HasMaxLength(255);
            });

            // Configure PaperProperty
            modelBuilder.Entity<PaperProperty>(entity =>
            {
                entity.HasKey(e => new { e.PaperId, e.PropertyId }).HasName("paper_properties_pkey");
                entity.ToTable("paper_properties");
                entity.HasIndex(e => e.PropertyId).HasDatabaseName("IX_paper_properties_property_id");
                entity.Property(e => e.PaperId).HasColumnName("paper_id");
                entity.Property(e => e.PropertyId).HasColumnName("property_id");
                entity.HasOne(e => e.Paper).WithMany(p => p.PaperProperties).HasForeignKey(e => e.PaperId).HasConstraintName("paper_properties_paper_id_fkey");
                entity.HasOne(e => e.Property).WithMany(p => p.PaperProperties).HasForeignKey(e => e.PropertyId).HasConstraintName("paper_properties_property_id_fkey");
            });

            // Configure Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orders_pkey");
                entity.ToTable("orders");
                entity.HasIndex(e => e.CustomerId).HasDatabaseName("IX_orders_customer_id");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderDate).HasColumnName("order_date").HasDefaultValueSql("current_timestamp");
                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
                entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasDefaultValue("pending").HasMaxLength(50);
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount").IsRequired();
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.HasOne(e => e.Customer).WithMany(c => c.Orders).HasForeignKey(e => e.CustomerId).HasConstraintName("orders_customer_id_fkey").OnDelete(DeleteBehavior.Cascade);
            });

            // Configure OrderEntry
            modelBuilder.Entity<OrderEntry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("order_entries_pkey");
                entity.ToTable("order_entries");
                entity.HasIndex(e => e.OrderId).HasDatabaseName("IX_order_entries_order_id");
                entity.HasIndex(e => e.ProductId).HasDatabaseName("IX_order_entries_product_id");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.HasOne(e => e.Product).WithMany(p => p.OrderEntries).HasForeignKey(e => e.ProductId).HasConstraintName("order_entries_product_id_fkey");
                entity.HasOne(e => e.Order).WithMany(o => o.OrderEntries).HasForeignKey(e => e.OrderId).HasConstraintName("order_entries_order_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}