using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

namespace ProductBox_Task.Models
{
    public partial class  Conn_DBContext : DbContext
    {
        public Conn_DBContext()
        {
        }
        public Conn_DBContext(DbContextOptions<Conn_DBContext> options)
           : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=JEHAN;Initial Catalog=ProductBox_Task;Integrated Security=True;MultipleActiveResultSets=true");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");


                entity.Property(e => e.Name)
                    .HasColumnName("Name");

                entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeId");

                entity.Property(e => e.Description).IsRequired().HasColumnName("Description");

                entity.Property(e => e.Address).HasColumnName("Address");
                entity.Property(e => e.City).HasColumnName("City");
                entity.Property(e => e.State).HasColumnName("State");
                entity.Property(e => e.Zip).HasColumnName("Zip");
                entity.Property(e => e.LastUpdated).HasColumnName("LastUpdated");

                entity.HasOne(e => e.CustomerType)
                       .WithMany(p => p.Customers)
                       .HasForeignKey(d => d.CustomerTypeId)
                       .HasConstraintName("FK_Customer_CustomerType");

            });
            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.ToTable("CustomerType");

                entity.Property(e => e.Id).HasColumnName("Id");


                entity.Property(e => e.Name)
                    .HasColumnName("Name");

            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
