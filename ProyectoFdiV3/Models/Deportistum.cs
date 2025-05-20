using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFdiV3.Models;

public partial class Deportistum
{
    [Key]
    public int IdDep { get; set; }

    public string? NombresDep { get; set; }

    public string? ApellidosDep { get; set; }

    public string? CedulaDep { get; set; }

    public bool? ActivoDep { get; set; }


    public int? IdGen { get; set; }

    public int? IdClub { get; set; }

    public int? IdEnt { get; set; }


    [ForeignKey("IdClub")] // Indica que CompetenciaSede está relacionada con IdSede
    public virtual Club? DeportistaClub { get; set; }

    public virtual Entrenador? IdEntNavigation { get; set; }

    public virtual Genero? IdGenNavigation { get; set; }

    public virtual Provincium? IdProNavigation { get; set; }

    public virtual Usuario? IdUsuNavigation { get; set; }

    public virtual ICollection<RegistroResultado>? RegistrosResultados { get; set; }


    //public virtual ICollection<CompetenciaDeportista> CompetenciaDeportistas { get; set; } = new List<CompetenciaDeportista>();


}
