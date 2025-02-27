using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    public virtual DetalleCompetencium DetalleCompetencia { get; } = new DetalleCompetencium();




    public virtual Categorium? IdCatNavigation { get; set; }

    //public virtual Genero? IdGenNavigation { get; set; }

    public virtual Juez? IdJuezNavigation { get; set; }

    public virtual Modalidad? IdModNavigation { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }

    public virtual ICollection<CompetenciaDeportista> CompetenciaDeportistas { get; set; } = new List<CompetenciaDeportista>();

    public virtual ICollection<RegistroResultado>? RegistrosResultados { get; set; }

}
