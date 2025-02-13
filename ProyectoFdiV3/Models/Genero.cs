using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class Genero
{
    [Key]
    public int IdGen { get; set; }

    public string? NombreGen { get; set; }

    //public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();

    //public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();
}
