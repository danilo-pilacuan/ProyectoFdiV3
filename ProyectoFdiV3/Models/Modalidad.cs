using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class Modalidad
{
    [Key]
    public int IdMod { get; set; }

    public string? DescripcionMod { get; set; }

    public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();

    
}
