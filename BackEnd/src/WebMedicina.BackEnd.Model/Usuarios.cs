using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Usuarios
{
    public uint IdUsuario { get; set; }

    public DateTime FechaCreac { get; set; }

    public DateTime FechaUltMod { get; set; }

    public virtual Passwords IdUsuario1 { get; set; } = null!;

    public virtual Medicos IdUsuarioNavigation { get; set; } = null!;
}
