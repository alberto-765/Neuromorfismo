using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class Farmacos
{
    public int IdFarmaco { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaCreac { get; set; }

    public DateOnly FechaUltMod { get; set; }
}
