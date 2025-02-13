using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class Provincium
{
    [Key]
    public int IdPro { get; set; }

    public string? NombrePro { get; set; }

    //public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();

    //public virtual ICollection<Entrenador> Entrenadors { get; } = new List<Entrenador>();

    //public virtual ICollection<Juez> Juezs { get; } = new List<Juez>();
}
