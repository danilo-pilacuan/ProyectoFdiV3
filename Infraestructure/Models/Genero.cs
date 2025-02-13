﻿using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Genero
{
    public int IdGen { get; set; }

    public string? NombreGen { get; set; }

    public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();

    public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();
}
