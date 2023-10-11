using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class AspnetroleModel
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspnetroleclaimModel> Aspnetroleclaims { get; set; } = new List<AspnetroleclaimModel>();

    public virtual ICollection<AspnetuserModel> Users { get; set; } = new List<AspnetuserModel>();
}
