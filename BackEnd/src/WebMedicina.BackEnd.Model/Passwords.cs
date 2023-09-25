using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Passwords
{
    public uint IdUsuario { get; set; }

    public string Password1 { get; set; } = null!;

    public virtual Usuarios? Usuario { get; set; }
}
