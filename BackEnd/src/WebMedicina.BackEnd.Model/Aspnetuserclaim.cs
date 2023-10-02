using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

/// <summary>
/// Almacena información adicional sobre los usuarios, como identidades externas (por ejemplo, autenticación con Google o Facebook) y otros datos personalizados.
/// </summary>
public partial class Aspnetuserclaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual Aspnetuser User { get; set; } = null!;
}
