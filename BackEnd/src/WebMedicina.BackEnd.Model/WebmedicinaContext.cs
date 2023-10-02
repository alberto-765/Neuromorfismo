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

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Epilepsia> Epilepsias { get; set; }

    public virtual DbSet<Farmaco> Farmacos { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Medicospaciente> Medicospacientes { get; set; }

    public virtual DbSet<Mutacione> Mutaciones { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles", tb => tb.HasComment("Aquí se guardan los roles de usuario que puedes definir para tu aplicación. Los roles permiten agrupar usuarios y asignarles permisos específicos."));

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers", tb => tb.HasComment("Esta tabla almacena la informacion de los usuarios registrados, como nombres de usuario, direcciones de correo electronico, contrasennas con hash, etc."));

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.AccessFailedCount)
                .HasComment("Contador de logins fallidos")
                .HasColumnType("int(11)");
            entity.Property(e => e.ConcurrencyStamp).HasComment("Su proposito principal es prevenir problemas de concurrencia cuando varios procesos o hilos intentan actualizar el mismo registro de usuario al mismo tiempo.");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnabled).HasComment("Bloqueo del usuario");
            entity.Property(e => e.LockoutEnd)
                .HasMaxLength(6)
                .HasComment("Fecha fin de bloqueo");
            entity.Property(e => e.NormalizedEmail)
                .HasMaxLength(256)
                .HasComment("Version en mayusculas");
            entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(256)
                .HasComment("Version en mayusculas");
            entity.Property(e => e.SecurityStamp).HasComment("Sello del usuario que se verifica al hacer login, cambiar contraseña..");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("aspnetuserroles", tb => tb.HasComment("Esta tabla asocia usuarios a roles. Permite establecer qué usuarios tienen acceso a qué recursos y funcionalidades de la aplicación."));
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims", tb => tb.HasComment("Almacena información adicional sobre los usuarios, como identidades externas (por ejemplo, autenticación con Google o Facebook) y otros datos personalizados."));

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

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("aspnetusertokens");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Epilepsia>(entity =>
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

        modelBuilder.Entity<Farmaco>(entity =>
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

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity
                .ToTable("medicos")
                .UseCollation("utf8mb4_spanish2_ci");

            entity.HasIndex(e => e.NumHistoria, "numHistoria").IsUnique();

            entity.Property(e => e.IdUsuario)
                .HasColumnName("idUsuario")
                .UseCollation("utf8mb4_general_ci");
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

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Medico)
                .HasForeignKey<Medico>(d => d.IdUsuario)
                .HasConstraintName("FK_medicos_aspnetusers");
        });

        modelBuilder.Entity<Medicospaciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medicospacientes", tb => tb.HasComment("Relacion de que medicos pueden editar que pacientes"));

            entity.HasIndex(e => e.IdPaciente, "FK_medicospacientes_pacientes");

            entity.HasIndex(e => new { e.IdUsuario, e.IdPaciente }, "idUsuario_idPaciente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPaciente)
                .HasColumnType("int(11)")
                .HasColumnName("idPaciente");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Medicospacientes)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK_medicospacientes_pacientes");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Medicospacientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_medicospacientes_aspnetusers");
        });

        modelBuilder.Entity<Mutacione>(entity =>
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

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity
                .ToTable("pacientes")
                .UseCollation("utf8mb4_spanish2_ci");

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
            entity.Property(e => e.MedicoUltMod)
                .HasColumnType("int(11)")
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pacientes_epilepsias");

            entity.HasOne(d => d.IdFarmacoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdFarmaco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pacientes_farmacos");

            entity.HasOne(d => d.IdMutacionNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdMutacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pacientes_mutaciones");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
