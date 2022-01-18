using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecaAPi.Data.Models;

#nullable disable

namespace RecaAPi.Data
{
    public partial class RecaContext : DbContext
    {
        public RecaContext()
        {
        }

        public RecaContext(DbContextOptions<RecaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fruta> Frutas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SQL5109.site4now.net;Database=db_a7f428_recadb;uid=db_a7f428_recadb_admin;pwd=ArRePa19;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Fruta>(entity =>
            {
                entity.ToTable("FRUTAS");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaModificacion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Precios)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
