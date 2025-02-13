using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class Sede
{
    [Key]
    public int IdSede { get; set; }

    public string? NombreSede { get; set; }

    public virtual ICollection<Competencium> Competencias { get; } = new List<Competencium>();
}
