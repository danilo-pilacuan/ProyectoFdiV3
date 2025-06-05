using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public int? IdUsuNavigationIdUsu { get; set; }


    public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();

    [ForeignKey("IdPro")]
    public virtual Provincium? IdProNavigation { get; set; }
    [ForeignKey("IdUsuNavigationIdUsu")]

    public virtual Usuario? IdUsuNavigation { get; set; }
}
