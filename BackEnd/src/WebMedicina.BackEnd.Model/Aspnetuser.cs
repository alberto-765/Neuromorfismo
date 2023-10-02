using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

/// <summary>
/// Esta tabla almacena la informacion de los usuarios registrados, como nombres de usuario, direcciones de correo electronico, contrasennas con hash, etc.
/// </summary>
public partial class Aspnetuser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    /// <summary>
    /// Version en mayusculas
    /// </summary>
    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    /// <summary>
    /// Version en mayusculas
    /// </summary>
    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    /// <summary>
    /// Sello del usuario que se verifica al hacer login, cambiar contraseña..
    /// </summary>
    public string? SecurityStamp { get; set; }

    /// <summary>
    /// Su proposito principal es prevenir problemas de concurrencia cuando varios procesos o hilos intentan actualizar el mismo registro de usuario al mismo tiempo.
    /// </summary>
    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Fecha fin de bloqueo
    /// </summary>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    /// Bloqueo del usuario
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Contador de logins fallidos
    /// </summary>
    public int AccessFailedCount { get; set; }

    public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; } = new List<Aspnetuserclaim>();

    public virtual ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; } = new List<Aspnetuserlogin>();

    public virtual ICollection<Aspnetusertoken> Aspnetusertokens { get; set; } = new List<Aspnetusertoken>();

    public virtual Medico? Medico { get; set; }

    public virtual ICollection<Medicospaciente> Medicospacientes { get; set; } = new List<Medicospaciente>();

    public virtual ICollection<Aspnetrole> Roles { get; set; } = new List<Aspnetrole>();
}
