using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models;

public partial class DetalleCompetencium
{
    [Key]
    public int IdDetalle { get; set; }

    //public int? Puesto { get; set; }

    public string? ClasRes { get; set; }

    public string? OctavosRes { get; set; }

    public string? CuartosRes { get; set; }

    public string? SemiRes { get; set; }

    public string? FinalRes { get; set; }

    
    public virtual Competencium? IdComNavigation { get; set; }

}
