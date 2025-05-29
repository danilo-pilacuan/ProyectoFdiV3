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
    public int? IdProvincia { get; set; }
    public int? IdUsuario { get; set; }


    [ForeignKey("IdClub")] // Indica que CompetenciaSede está relacionada con IdSede
    public virtual Club? Club { get; set; }
    [ForeignKey("IdEnt")]
    public virtual Entrenador? Entrenador { get; set; }
    [ForeignKey("IdGen")]
    public virtual Genero? Genero { get; set; }
    [ForeignKey("IdProvincia")]
    public virtual Provincium? Provincia { get; set; }
    [ForeignKey("IdUsuario")]
    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<RegistroResultado>? RegistrosResultados { get; set; }


    //public virtual ICollection<CompetenciaDeportista> CompetenciaDeportistas { get; set; } = new List<CompetenciaDeportista>();

    /*
     
     public int? IdSede { get; set; }

    public int? IdMod { get; set; }

    public int? NumPresas {get; set;}

    public int? NumPresasR1ClasifVias { get; set; }
    public int? NumPresasR2ClasifVias { get; set; }
    public int? NumPresasR1FinalVias { get; set; }
    public int? NumPresasR2FinalVias { get; set; }


    public virtual DetalleCompetencium DetalleCompetencia { get; } = new DetalleCompetencium();




    public virtual Categorium? IdCatNavigation { get; set; }

    //public virtual Genero? IdGenNavigation { get; set; }

    public virtual Juez? IdJuezNavigation { get; set; }

    public virtual Modalidad? IdModNavigation { get; set; }

    [ForeignKey("IdSede")] // Indica que CompetenciaSede está relacionada con IdSede

    public virtual Sede? CompetenciaSede { get; set; }
     */
}
