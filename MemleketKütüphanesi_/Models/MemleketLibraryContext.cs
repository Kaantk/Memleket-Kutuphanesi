using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MemleketKütüphanesi_.Models;

public partial class MemleketLibraryContext : DbContext
{
    public MemleketLibraryContext()
    {
    }

    public MemleketLibraryContext(DbContextOptions<MemleketLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-NQ50JJQ\\SQLEXPRESS;Database=MemleketLibrary;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(20);
            entity.Property(e => e.DateOfIssued).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PublishHouse).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasColumnType("ntext");
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(25);
            entity.Property(e => e.Gender).HasMaxLength(6);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.Surname).HasMaxLength(15);
            entity.Property(e => e.TcNo).HasMaxLength(11);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(25);
            entity.Property(e => e.Gender).HasMaxLength(6);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.PhoneNumber).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(15);
            entity.Property(e => e.TcNo).HasMaxLength(11);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
