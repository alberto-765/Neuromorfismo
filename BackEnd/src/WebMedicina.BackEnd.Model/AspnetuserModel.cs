using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class AspnetuserModel
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; } = new List<Aspnetuserclaim>();

    public virtual ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; } = new List<Aspnetuserlogin>();

    public virtual ICollection<AspnetusertokenModel> Aspnetusertokens { get; set; } = new List<AspnetusertokenModel>();

    public virtual ICollection<MedicosModel> Medicos { get; set; } = new List<MedicosModel>();

    public virtual ICollection<AspnetroleModel> Roles { get; set; } = new List<AspnetroleModel>();
}
