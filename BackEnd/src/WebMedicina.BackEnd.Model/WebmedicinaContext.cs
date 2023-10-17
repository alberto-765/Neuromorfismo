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

    public virtual DbSet<AspnetroleModel> Aspnetroles { get; set; }

    public virtual DbSet<AspnetroleclaimModel> Aspnetroleclaims { get; set; }

    public virtual DbSet<AspnetuserModel> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<AspnetusertokenModel> Aspnetusertokens { get; set; }

    public virtual DbSet<EpilepsiaModel> Epilepsias { get; set; }

    public virtual DbSet<FarmacosModel> Farmacos { get; set; }

    public virtual DbSet<MedicosModel> Medicos { get; set; }

    public virtual DbSet<MedicospacienteModel> Medicospacientes { get; set; }

    public virtual DbSet<MutacionesModel> Mutaciones { get; set; }

    public virtual DbSet<PacientesModel> Pacientes { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AspnetroleModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspnetroleclaimModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<AspnetuserModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.AccessFailedCount).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<AspnetroleModel>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<AspnetuserModel>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<AspnetusertokenModel>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("aspnetusertokens");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<EfmigrationshistoryModel>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<EpilepsiaModel>(entity =>
        {
            entity.HasKey(e => e.IdEpilepsia).HasName("PRIMARY");

            entity
                .ToTable("epilepsias")
                .UseCollation("utf8mb4_spanish2_ci");

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

        modelBuilder.Entity<FarmacosModel>(entity =>
        {
            entity.HasKey(e => e.IdFarmaco).HasName("PRIMARY");

            entity
                .ToTable("farmacos")
                .UseCollation("utf8mb4_spanish2_ci");

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

        modelBuilder.Entity<MedicosModel>(entity =>
        {
            entity.HasKey(e => e.NumHistoria).HasName("PRIMARY");

            entity
                .ToTable("medicos")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.HasIndex(e => e.NetuserId, "Índice 2");

            entity.Property(e => e.NumHistoria)
                .HasMaxLength(50)
                .HasColumnName("numHistoria");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaCreac).HasColumnName("fechaCreac");
            entity.Property(e => e.FechaNac)
                .HasColumnType("datetime")
                .HasColumnName("fechaNac");
            entity.Property(e => e.FechaUltMod)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fechaUltMod");
            entity.Property(e => e.NetuserId)
                .HasColumnName("netuserId")
                .UseCollation("utf8mb4_general_ci");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .HasDefaultValueSql("''")
                .HasColumnName("sexo");

            entity.HasOne(d => d.Netuser).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.NetuserId)
                .HasConstraintName("FK_medicos_aspnetusers");
        });

        modelBuilder.Entity<MedicospacienteModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medicospacientes", tb => tb.HasComment("Relacion de que medicos pueden editar que pacientes"));

            entity.HasIndex(e => e.IdPaciente, "FK_medicospacientes_pacientes");

            entity.HasIndex(e => new { e.IdMedico, e.IdPaciente }, "idUsuario_idPaciente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdMedico)
                .HasColumnName("idMedico")
                .UseCollation("utf8mb4_spanish2_ci");
            entity.Property(e => e.IdPaciente)
                .HasColumnType("int(11)")
                .HasColumnName("idPaciente");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.Medicospacientes)
                .HasForeignKey(d => d.IdMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_medicospacientes_medicos");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Medicospacientes)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK_medicospacientes_pacientes");
        });

        modelBuilder.Entity<MutacionesModel>(entity =>
        {
            entity.HasKey(e => e.IdMutacion).HasName("PRIMARY");

            entity
                .ToTable("mutaciones")
                .UseCollation("utf8mb4_spanish2_ci");

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

        modelBuilder.Entity<PacientesModel>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity
                .ToTable("pacientes")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.HasIndex(e => e.IdFarmaco, "idFarmaco");

            entity.HasIndex(e => e.IdMutacion, "idMutacion");

            entity.HasIndex(e => e.IdEpilepsia, "idTipoEpilepsia");

            entity.HasIndex(e => e.MedicoCreador, "medicoCreador");

            entity.HasIndex(e => e.MedicoUltMod, "medicoUltMod");

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
            entity.Property(e => e.MedicoCreador)
                .HasMaxLength(50)
                .HasColumnName("medicoCreador");
            entity.Property(e => e.MedicoUltMod)
                .HasMaxLength(50)
                .HasColumnName("medicoUltMod");
            entity.Property(e => e.Sexo)
                .HasDefaultValueSql("'H'")
                .HasColumnType("enum('H','M')")
                .HasColumnName("sexo");
            entity.Property(e => e.Talla)
                .HasPrecision(20, 6)
                .HasColumnName("talla");

            entity.HasOne(d => d.IdEpilepsiaNavigation).WithMany(p => p.Pacientes)
                 .HasForeignKey(d => d.IdEpilepsia)
                 .OnDelete(DeleteBehavior.SetNull)
                 .HasConstraintName("FK_pacientes_epilepsias");

            entity.HasOne(d => d.IdFarmacoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdFarmaco)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_farmacos");

            entity.HasOne(d => d.IdMutacionNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdMutacion)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_mutaciones");

            entity.HasOne(d => d.MedicoCreadorNavigation).WithMany(p => p.PacienteMedicoCreadorNavigations)
                .HasForeignKey(d => d.MedicoCreador)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_medicos_2");

            entity.HasOne(d => d.MedicoUltModNavigation).WithMany(p => p.PacienteMedicoUltModNavigations)
                .HasForeignKey(d => d.MedicoUltMod)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_pacientes_medicos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
