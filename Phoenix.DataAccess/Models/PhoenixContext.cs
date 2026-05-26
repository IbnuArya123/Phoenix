using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Phoenix.DataAccess.Models
{
    public partial class PhoenixContext : DbContext
    {
        public PhoenixContext()
        {
        }

        public PhoenixContext(DbContextOptions<PhoenixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Guest> Guests { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomInventory> RoomInventories { get; set; } = null!;
        public virtual DbSet<RoomService> RoomServices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS02;Initial Catalog=Phoenix;Trusted_Connection=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Administ__536C85E5A6CF9F69");

                entity.ToTable("Administrator");

                entity.Property(e => e.Username)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Guest__536C85E5F1AEAB3F");

                entity.ToTable("Guest");

                entity.Property(e => e.Username)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.BirtDate).HasColumnType("datetime");

                entity.Property(e => e.Citizenship)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Inventor__737584F749DD8B1C");

                entity.ToTable("Inventory");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Reservat__A25C5AA62EFB9A18");

                entity.ToTable("Reservation");

                entity.Property(e => e.Code)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BookDate).HasColumnType("datetime");

                entity.Property(e => e.CheckIn).HasColumnType("datetime");

                entity.Property(e => e.CheckOut).HasColumnType("datetime");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.GuestUsername)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReservationMethod)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.GuestUsernameNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.GuestUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__Guest__5DCAEF64");

                entity.HasOne(d => d.RoomNumberNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__RoomN__5CD6CB2B");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK__Room__78A1A19C2CB222CD");

                entity.ToTable("Room");

                entity.Property(e => e.Number)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RoomType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoomInventory>(entity =>
            {
                entity.ToTable("RoomInventory");

                entity.Property(e => e.InventoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.InventoryNameNavigation)
                    .WithMany(p => p.RoomInventories)
                    .HasForeignKey(d => d.InventoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoomInven__Inven__5535A963");

                entity.HasOne(d => d.RoomNumberNavigation)
                    .WithMany(p => p.RoomInventories)
                    .HasForeignKey(d => d.RoomNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoomInven__RoomN__5441852A");
            });

            modelBuilder.Entity<RoomService>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber)
                    .HasName("PK__RoomServ__8D663599869C086C");

                entity.ToTable("RoomService");

                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OutsourcingCompany)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
