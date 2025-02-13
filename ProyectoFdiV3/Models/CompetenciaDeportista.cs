using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models
{
    public class CompetenciaDeportista
    {
        [Key]
        public int Id { get; set; }

        // Relaciones de navegación
        public virtual Competencium Competencia { get; set; } = null!;
        public virtual Deportistum Deportista { get; set; } = null!;
    }
}
