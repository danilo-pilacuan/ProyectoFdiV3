using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Modalidad
{
    public int IdMod { get; set; }

    public string? DescripcionMod { get; set; }

    public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();

    
}
