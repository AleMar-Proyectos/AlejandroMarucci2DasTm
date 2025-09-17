using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Clase_entity_Martina.Models;

public partial class CrudWindowsFormContext : DbContext
{
    public CrudWindowsFormContext()
    {
    }

    public CrudWindowsFormContext(DbContextOptions<CrudWindowsFormContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Entity> Entities { get; set; }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=OMEN\\SQLEXPRESS;Initial Catalog=CrudWindowsForm;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alumnos__3213E83FAC152A02");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nonbre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nonbre");
            entity.Property(e => e.Nota).HasColumnName("nota");
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.ToTable("entity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("correo");
            entity.Property(e => e.Fechanacimiento)
                .HasColumnType("datetime")
                .HasColumnName("fechanacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("people");

            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
