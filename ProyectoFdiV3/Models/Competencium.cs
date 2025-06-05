using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFdiV3.Models;

public partial class Competencium
{
    [Key]
    public int IdCom { get; set; }

    public string? NombreCom { get; set; }

    public DateTime? FechaInicioCom { get; set; }

    public DateTime? FechaFinCom { get; set; }

    public bool? ActivoCom { get; set; }



    public int? IdJuez { get; set; }


    public int? IdSede { get; set; }

    public int? IdMod { get; set; }

    public int? NumPresas {get; set;}

    public int? IdCatNavigationIdCat { get; set; }


    public int? NumPresasR1ClasifVias { get; set; }
    public int? NumPresasR2ClasifVias { get; set; }
    public int? NumPresasR1FinalVias { get; set; }
    public int? NumPresasR2FinalVias { get; set; }


    public virtual DetalleCompetencium DetalleCompetencia { get; } = new DetalleCompetencium();



    [ForeignKey("IdCatNavigationIdCat")]

    public virtual Categorium? IdCatNavigation { get; set; }

    //public virtual Genero? IdGenNavigation { get; set; }

    public virtual Juez? IdJuezNavigation { get; set; }

    public virtual Modalidad? IdModNavigation { get; set; }

    [ForeignKey("IdSede")] // Indica que CompetenciaSede está relacionada con IdSede

    public virtual Sede? CompetenciaSede { get; set; }

    public virtual ICollection<CompetenciaDeportista> CompetenciaDeportistas { get; set; } = new List<CompetenciaDeportista>();

    public virtual ICollection<RegistroResultado>? RegistrosResultados { get; set; }

}
