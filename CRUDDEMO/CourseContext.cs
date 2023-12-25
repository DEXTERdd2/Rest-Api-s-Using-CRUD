using System;
using System.Collections.Generic;
using CRUDDEMO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUDDEMO
{
    public partial class CourseContext : DbContext
    {
        public CourseContext()
        {
        }

        public CourseContext(DbContextOptions<CourseContext> options)
            : base(options)
        {
        }
        

        public virtual DbSet<Market> Markets { get; set; } = null!;
        public virtual DbSet<Stores> Stores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS; Initial Catalog=Course; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover= False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Market>(entity =>
            {
                entity.HasKey(e => e.StorId)
                    .HasName("PK__market__347B83C4E1F3A982");

                entity.ToTable("market");

                entity.Property(e => e.StorId).HasColumnName("Stor_Id");

                entity.Property(e => e.StorAddress).HasColumnName("Stor_Address");

                entity.Property(e => e.StorName).HasColumnName("Stor_Name");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.StorId)
                    .HasName("PK__Store__347B83C441F5E291");

                entity.ToTable("Store");

                entity.Property(e => e.StorId).HasColumnName("Stor_Id");

                entity.Property(e => e.StorAddress).HasColumnName("Stor_Address");

                entity.Property(e => e.StorName).HasColumnName("Stor_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
