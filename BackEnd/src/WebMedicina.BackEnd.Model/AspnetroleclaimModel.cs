using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class AspnetroleclaimModel
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AspnetroleModel Role { get; set; } = null!;
}
