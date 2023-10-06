using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Medico
{
    public string IdUsuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly FechaNac { get; set; }

    public string Sexo { get; set; } = null!;

    public string NumHistoria { get; set; }

    public DateOnly FechaCreac { get; set; }

    public DateOnly FechaUltMod { get; set; }

    public virtual Aspnetuser IdUsuarioNavigation { get; set; } = null!;
}
