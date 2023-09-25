using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebMedicina.BackEnd.Model;

public partial class WebmedicinaContext : DbContext
{
    public WebmedicinaContext()
    {
    }

    public WebmedicinaContext(DbContextOptions<WebmedicinaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Epilepsias> Epilepsias { get; set; }

    public virtual DbSet<Farmacos> Farmacos { get; set; }

    public virtual DbSet<Medicos> Medicos { get; set; }

    public virtual DbSet<Mutaciones> Mutaciones { get; set; }

    public virtual DbSet<Pacientes> Pacientes { get; set; }

    public virtual DbSet<Passwords> Passwords { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_spanish2_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Epilepsias>(entity =>
        {
            entity.HasKey(e => e.IdEpilepsia).HasName("PRIMARY");

            entity.ToTable("epilepsias");

            entity.Property(e => e.IdEpilepsia)
                .HasColumnType("int(11)")
                .HasColumnName("idEpilepsia");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Farmacos>(entity =>
        {
            entity.HasKey(e => e.IdFarmaco).HasName("PRIMARY");

            entity.ToTable("farmacos");

            entity.Property(e => e.IdFarmaco)
                .HasColumnType("int(11)")
                .HasColumnName("idFarmaco");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Medicos>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("medicos");

            entity.HasIndex(e => e.NumHistoria, "numHistoria").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnType("int(6) unsigned zerofill")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaNac).HasColumnName("fechaNac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.NumHistoria)
                .HasColumnType("int(11)")
                .HasColumnName("numHistoria");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .HasDefaultValueSql("''")
                .IsFixedLength()
                .HasColumnName("sexo");
        });

        modelBuilder.Entity<Mutaciones>(entity =>
        {
            entity.HasKey(e => e.IdMutacion).HasName("PRIMARY");

            entity.ToTable("mutaciones");

            entity.Property(e => e.IdMutacion)
                .HasColumnType("int(11)")
                .HasColumnName("idMutacion");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pacientes>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity.ToTable("pacientes");

            entity.HasIndex(e => e.IdFarmaco, "idFarmaco");

            entity.HasIndex(e => e.IdMutacion, "idMutacion");

            entity.HasIndex(e => e.IdEpilepsia, "idTipoEpilepsia");

            entity.Property(e => e.IdPaciente)
                .HasColumnType("int(11)")
                .HasColumnName("idPaciente");
            entity.Property(e => e.DescripEnferRaras)
                .HasDefaultValueSql("''")
                .HasColumnType("text")
                .HasColumnName("descripEnferRaras");
            entity.Property(e => e.EnfermRaras)
                .HasColumnType("enum('S','N')")
                .HasColumnName("enfermRaras");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaDiagnostico)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaDiagnostico");
            entity.Property(e => e.FechaFractalidad)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaFractalidad");
            entity.Property(e => e.FechaNac)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaNac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.IdEpilepsia)
                .HasColumnType("int(11)")
                .HasColumnName("idEpilepsia");
            entity.Property(e => e.IdFarmaco)
                .HasColumnType("int(11)")
                .HasColumnName("idFarmaco");
            entity.Property(e => e.IdMutacion)
                .HasColumnType("int(11)")
                .HasColumnName("idMutacion");
            entity.Property(e => e.Sexo)
                .HasDefaultValueSql("'H'")
                .HasColumnType("enum('H','M')")
                .HasColumnName("sexo");
            entity.Property(e => e.Talla)
                .HasPrecision(20, 6)
                .HasColumnName("talla");
        });

        modelBuilder.Entity<Passwords>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("passwords");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnType("int(6) unsigned zerofill")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Password1)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(6) unsigned zerofill")
                .HasColumnName("idUsuario");
            entity.Property(e => e.FechaCreac)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreac");
            entity.Property(e => e.FechaUltMod)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaUltMod");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuarios>(d => d.IdUsuario)
                .HasConstraintName("FK_usuarios_medicos");

            entity.HasOne(d => d.IdUsuario1).WithOne(p => p.Usuario)
                .HasForeignKey<Usuarios>(d => d.IdUsuario)
                .HasConstraintName("FK_usuarios_passwords");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
