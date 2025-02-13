using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class Juez
{
    [Key]
    public int IdJuez { get; set; }

    public string? NombresJuez { get; set; }

    public string? ApellidosJuez { get; set; }

    public string? CedulaJuez { get; set; }

    public bool? PrincipalJuez { get; set; }

    public bool? ActivoJuez { get; set; }

    public int? IdPro { get; set; }

    

    public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();

    public virtual Provincium? IdProNavigation { get; set; }

    public virtual Usuario? IdUsuNavigation { get; set; }
}
