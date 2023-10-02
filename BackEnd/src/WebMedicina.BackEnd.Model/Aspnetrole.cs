using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

/// <summary>
/// Aquí se guardan los roles de usuario que puedes definir para tu aplicación. Los roles permiten agrupar usuarios y asignarles permisos específicos.
/// </summary>
public partial class Aspnetrole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<Aspnetroleclaim> Aspnetroleclaims { get; set; } = new List<Aspnetroleclaim>();

    public virtual ICollection<Aspnetuser> Users { get; set; } = new List<Aspnetuser>();
}
