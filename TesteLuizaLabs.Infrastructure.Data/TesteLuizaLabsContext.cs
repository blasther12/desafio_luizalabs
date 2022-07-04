using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Infrastructure.Data
{
    public class TesteLuizaLabsContext : DbContext
    {
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public TesteLuizaLabsContext(DbContextOptions<TesteLuizaLabsContext> options) : base(options) { }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerProduct> CustomerProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(entity.Property<long>("Id"));

                entity.Property<DateTime>("Date")
                    .HasColumnType("timestamp with time zone");

                entity.Property<string>("Email")
                    .HasColumnType("text");

                entity.Property<string>("Name")
                    .HasColumnType("text");

                entity.HasKey("Id");

                entity.HasIndex("Id");

                entity.ToTable("customers", (string)null);
            });

            modelBuilder.Entity<CustomerProduct>(entity =>
            {
                entity.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(entity.Property<long>("Id"));

                entity.Property<long>("CustomerId")
                    .HasColumnType("bigint");

                entity.Property<DateTime>("Date")
                    .HasColumnType("timestamp with time zone");

                entity.Property<string>("Key")
                    .HasColumnType("text");

                entity.HasKey("Id");

                entity.HasIndex("CustomerId");

                entity.HasIndex("Id");

                entity.ToTable("customerproducts", (string)null);

            });

            modelBuilder.Entity("TesteLuizaLabs.Domain.Entity.CustomerProduct", b =>
            {
                b.HasOne("TesteLuizaLabs.Domain.Entity.Customer", "Customer")
                    .WithMany("CustomerProduct")
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

            modelBuilder.Entity("TesteLuizaLabs.Domain.Entity.Customer", b =>
            {
                b.Navigation("CustomerProduct");
            });
        }
    }
}
