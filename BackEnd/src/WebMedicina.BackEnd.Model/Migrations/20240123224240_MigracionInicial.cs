using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMedicina.BackEnd.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Epilepsias",
                columns: table => new
                {
                    idEpilepsia = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idEpilepsia);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Farmacos",
                columns: table => new
                {
                    idFarmaco = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idFarmaco);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mutaciones",
                columns: table => new
                {
                    idMutacion = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMutacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    idMedico = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userLogin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellidos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechaNac = table.Column<DateTime>(type: "datetime", nullable: false),
                    sexo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "''")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    netuserId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMedico);
                    table.ForeignKey(
                        name: "FK_medicos_aspnetusers",
                        column: x => x.netuserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EtapaLT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Label = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdMedicoCreador = table.Column<int>(type: "int(11)", nullable: true),
                    IdMedicoUltModif = table.Column<int>(type: "int(11)", nullable: true),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapaLT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapaLT_Medicos_IdMedicoCreador",
                        column: x => x.IdMedicoCreador,
                        principalTable: "Medicos",
                        principalColumn: "idMedico");
                    table.ForeignKey(
                        name: "FK_EtapaLT_Medicos_IdMedicoUltModif",
                        column: x => x.IdMedicoUltModif,
                        principalTable: "Medicos",
                        principalColumn: "idMedico");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    idPaciente = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumHistoria = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechaNac = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "curdate()"),
                    sexo = table.Column<string>(type: "enum('H','M')", nullable: false, defaultValueSql: "'H'")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    talla = table.Column<int>(type: "int", precision: 20, scale: 6, nullable: false),
                    fechaDiagnostico = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "curdate()"),
                    fechaFractalidad = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "curdate()"),
                    farmaco = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idEpilepsia = table.Column<int>(type: "int(11)", nullable: true),
                    idMutacion = table.Column<int>(type: "int(11)", nullable: true),
                    enfermRaras = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValueSql: "''")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripEnferRaras = table.Column<string>(type: "text", nullable: false, defaultValueSql: "''")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    medicoUltMod = table.Column<int>(type: "int(11)", nullable: false),
                    medicoCreador = table.Column<int>(type: "int(11)", nullable: false),
                    FechaCreac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltMod = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idPaciente);
                    table.ForeignKey(
                        name: "FK_pacientes_epilepsias",
                        column: x => x.idEpilepsia,
                        principalTable: "Epilepsias",
                        principalColumn: "idEpilepsia",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_pacientes_medicos",
                        column: x => x.medicoUltMod,
                        principalTable: "Medicos",
                        principalColumn: "idMedico");
                    table.ForeignKey(
                        name: "FK_pacientes_medicos_2",
                        column: x => x.medicoCreador,
                        principalTable: "Medicos",
                        principalColumn: "idMedico");
                    table.ForeignKey(
                        name: "FK_pacientes_mutaciones",
                        column: x => x.idMutacion,
                        principalTable: "Mutaciones",
                        principalColumn: "idMutacion",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdMedico = table.Column<int>(type: "int(11)", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_UserRefreshToken_Medicos_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EvolucionLT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Confirmado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IdMedicoUltModif = table.Column<int>(type: "int(11)", nullable: false),
                    IdEtapa = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolucionLT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolucionLT_EtapaLT_IdEtapa",
                        column: x => x.IdEtapa,
                        principalTable: "EtapaLT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvolucionLT_Medicos_IdMedicoUltModif",
                        column: x => x.IdMedicoUltModif,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvolucionLT_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medicospacientes",
                columns: table => new
                {
                    idMedPac = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idMedico = table.Column<int>(type: "int(11)", nullable: false),
                    idPaciente = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMedPac);
                    table.ForeignKey(
                        name: "FK_medicospacientes_medicos",
                        column: x => x.idMedico,
                        principalTable: "Medicos",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicospacientes_pacientes",
                        column: x => x.idPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Relacion de que medicos pueden editar que pacientes")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "51dcd80f-b5d8-4ed6-b440-2d8e91041e1b", "superAdmin", "SUPERADMIN" },
                    { "2", "baea1625-824e-4507-aa00-10600451a2c2", "admin", "ADMIN" },
                    { "3", "74621536-7497-4222-9fc1-8fbe6e1e2b7e", "medico", "MEDICO" }
                });

            migrationBuilder.InsertData(
                table: "Epilepsias",
                columns: new[] { "idEpilepsia", "FechaCreac", "FechaUltMod", "nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "Epilepsia1" },
                    { 2, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "Epilepsia2" }
                });

            migrationBuilder.InsertData(
                table: "Mutaciones",
                columns: new[] { "idMutacion", "FechaCreac", "FechaUltMod", "nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "Mutacion1" },
                    { 2, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "Mutacion2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_IdMedicoCreador",
                table: "EtapaLT",
                column: "IdMedicoCreador");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaLT_IdMedicoUltModif",
                table: "EtapaLT",
                column: "IdMedicoUltModif");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_IdEtapa",
                table: "EvolucionLT",
                column: "IdEtapa");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_IdMedicoUltModif",
                table: "EvolucionLT",
                column: "IdMedicoUltModif");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucionLT_IdPaciente",
                table: "EvolucionLT",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "Índice 2",
                table: "Medicos",
                column: "netuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_netuserId",
                table: "Medicos",
                column: "netuserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "userLogin",
                table: "Medicos",
                column: "userLogin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_medicospacientes_pacientes",
                table: "medicospacientes",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "idUsuario_idPaciente",
                table: "medicospacientes",
                columns: new[] { "idMedico", "idPaciente" });

            migrationBuilder.CreateIndex(
                name: "idMutacion",
                table: "Pacientes",
                column: "idMutacion");

            migrationBuilder.CreateIndex(
                name: "idTipoEpilepsia",
                table: "Pacientes",
                column: "idEpilepsia");

            migrationBuilder.CreateIndex(
                name: "medicoCreador",
                table: "Pacientes",
                column: "medicoCreador");

            migrationBuilder.CreateIndex(
                name: "medicoUltMod",
                table: "Pacientes",
                column: "medicoUltMod");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshToken_IdMedico",
                table: "UserRefreshToken",
                column: "IdMedico",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EvolucionLT");

            migrationBuilder.DropTable(
                name: "Farmacos");

            migrationBuilder.DropTable(
                name: "medicospacientes");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EtapaLT");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Epilepsias");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Mutaciones");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
