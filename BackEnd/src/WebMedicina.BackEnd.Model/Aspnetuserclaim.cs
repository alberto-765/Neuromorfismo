using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Aspnetuserclaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AspnetuserModel User { get; set; } = null!;
}
