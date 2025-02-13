using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models
{
    public class RegistroResultado
    {
        [Key]
        public int IdRegistroResultado { get; set; }

        

        public int? IdDep { get; set; }
        public int? IdCom { get; set; }
        public float? Tiempo1 { get; set; }
        public float? Tiempo2 { get; set; }
        public int? Intento1 { get; set; }
        public int? Intento2 { get; set; }
        public int? Intento3 { get; set; }
        public bool Completado1 { get; set; }
        public bool Completado2 { get; set; }
        public bool Completado3 { get; set; }

        public int? MaxEscala1 { get; set; }
        public int? MaxEscala2 { get; set; }
        public int? MaxEscala3 { get; set; }

        public double PorcentajeAlcanzado1 { get; set; } // Porcentaje de la pared alcanzado
        public double PorcentajeAlcanzado2 { get; set; } // Porcentaje de la pared alcanzado
        public double PorcentajeAlcanzado3 { get; set; } // Porcentaje de la pared alcanzado
        public int UltimaPresa1 { get; set; } // Identificador o descripción de la última presa agarrada
        public int UltimaPresa2 { get; set; } // Identificador o descripción de la última presa agarrada
        public int UltimaPresa3 { get; set; } // Identificador o descripción de la última presa agarrada


        public int? Puesto { get; set; }

        public int? Etapa { get; set; }

        public bool RegistroCompleto { get; set; }

        public virtual Deportistum? IdDepNavigation { get; set; }
        public virtual Competencium? CompetenciumNavigation { get; set; }

    }
}
