using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class AspnetusertokenModel
{
    public string UserId { get; set; } = null!;

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual AspnetuserModel User { get; set; } = null!;
}
