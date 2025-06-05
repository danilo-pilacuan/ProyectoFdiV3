using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public int? IdUsuNavigationIdUsu { get; set; }



    public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();
    [ForeignKey("IdPro")]
    public virtual Provincium? IdProNavigation { get; set; }
    [ForeignKey("IdUsuNavigationIdUsu")]
    public virtual Usuario? IdUsuNavigation { get; set; }
}
