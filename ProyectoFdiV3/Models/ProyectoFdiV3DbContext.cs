using Microsoft.EntityFrameworkCore;

namespace ProyectoFdiV3.Models
{
    public class ProyectoFdiV3DbContext : DbContext
    {
        public ProyectoFdiV3DbContext(DbContextOptions<ProyectoFdiV3DbContext> options)
            : base(options)
        {
        }

        public DbSet<Categorium> Categorias { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Competencium> Competencias { get; set; }
        public DbSet<Deportistum> Deportistas { get; set; }
        public DbSet<DetalleCompetencium> DetalleCompetencias { get; set; }
        public DbSet<Entrenador> Entrenadores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Juez> Jueces { get; set; }
        public DbSet<Modalidad> Modalidades { get; set; }
        public DbSet<Provincium> Provincias { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CompetenciaDeportista> CompetenciaDeportistas { get; set; }

        // Agregar DbSet para RegistroResultado
        public DbSet<RegistroResultado> RegistroResultados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Deportista)
                .WithOne(d => d.Club)
                .HasForeignKey(d => d.IdClub);

            modelBuilder.Entity<Competencium>()
                .HasOne(c => c.IdJuezNavigation)
                .WithMany(j => j.Competencia)
                .HasForeignKey(c => c.IdJuez);

            modelBuilder.Entity<Competencium>()
                .HasOne(c => c.IdModNavigation)
                .WithMany(m => m.Competencia)
                .HasForeignKey(c => c.IdMod);

            modelBuilder.Entity<Deportistum>()
                .HasOne(d => d.Club)
                .WithMany(c => c.Deportista)
                .HasForeignKey(d => d.IdClub);

            modelBuilder.Entity<Deportistum>()
                .HasOne(d => d.Entrenador)
                .WithMany(e => e.Deportista)
                .HasForeignKey(d => d.IdEnt);

            // Relaciones para RegistroResultado
            modelBuilder.Entity<RegistroResultado>()
                .HasOne(r => r.Deportista)
                .WithMany(d => d.RegistrosResultados) // Asumiendo que en Deportistum tienes una colección de RegistroResultado
                .HasForeignKey(r => r.IdDep);

            modelBuilder.Entity<RegistroResultado>()
                .HasOne(r => r.Competencia)
                .WithMany(c => c.RegistrosResultados) // Asumiendo que en Competencium tienes una colección de RegistroResultado
                .HasForeignKey(r => r.IdCom); // Cambié a IdCom para coincidir con la propiedad en tu modelo

            modelBuilder.Entity<RegistroResultado>()
                .Property(r => r.PuntajePrevio)
                .HasDefaultValue(0.0f);
        }
    }
}
