using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFDI.API.V3.Models;

namespace Infraestructure.Models
{
    public class ProyectoFdiV3Context : DbContext
    {
        public ProyectoFdiV3Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public virtual DbSet<Categorium> Categoria { get; set; }

        public virtual DbSet<Club> Clubs { get; set; }

        public virtual DbSet<Competencium> Competencia { get; set; }

        public virtual DbSet<Deportistum> Deportista { get; set; }


        public virtual DbSet<DetalleCompetencium> DetalleCompetencia { get; set; }

        public virtual DbSet<Entrenador> Entrenadors { get; set; }

        public virtual DbSet<Genero> Generos { get; set; }

        public virtual DbSet<Juez> Juezs { get; set; }

        public virtual DbSet<Modalidad> Modalidads { get; set; }

        public virtual DbSet<Provincium> Provincia { get; set; }


        public virtual DbSet<Sede> Sedes { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<RegistroResultado> RegistrosResultado { get; set; } = null!;

    }
}
