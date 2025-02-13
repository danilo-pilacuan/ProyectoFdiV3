﻿using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Competencium
{
    public int IdCom { get; set; }

    public string? NombreCom { get; set; }

    public DateTime? FechaInicioCom { get; set; }

    public DateTime? FechaFinCom { get; set; }

    public bool? ActivoCom { get; set; }

    public int? IdGen { get; set; }

    public int? IdJuez { get; set; }

    public int? IdCat { get; set; }

    public int? IdSede { get; set; }

    public int? IdMod { get; set; }

    //public virtual ICollection<DetalleCompetencium> DetalleCompetencia { get; } = new List<DetalleCompetencium>();
    public virtual DetalleCompetencium DetalleCompetencia { get; } = new DetalleCompetencium();

    //public virtual ICollection<DetalleCompetenciaDificultad> DetalleCompetenciaDificultads { get; } = new List<DetalleCompetenciaDificultad>();

    public virtual Categorium? IdCatNavigation { get; set; }

    public virtual Genero? IdGenNavigation { get; set; }

    public virtual Juez? IdJuezNavigation { get; set; }

    public virtual Modalidad? IdModNavigation { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }
    //public virtual ICollection<Boulder> Boulders { get; } = new List<Boulder>();
    //public virtual ICollection<Dificultad> Dificultads { get; } = new List<Dificultad>();

    //public virtual ICollection<PuntajeBloque> PuntajeBloques { get; } = new List<PuntajeBloque>();

    //public virtual ICollection<ResultadoBloque> ResultadoBloques { get; } = new List<ResultadoBloque>();
}
