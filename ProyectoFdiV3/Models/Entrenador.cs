﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class Entrenador
{
    [Key]
    public int IdEnt { get; set; }

    public string? NombresEnt { get; set; }

    public string? ApellidosEnt { get; set; }

    public string? CedulaEnt { get; set; }

    public bool? ActivoEnt { get; set; }

    public int? IdPro { get; set; }


    public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();

    public virtual Provincium? IdProNavigation { get; set; }

    public virtual Usuario? IdUsuNavigation { get; set; }
}
