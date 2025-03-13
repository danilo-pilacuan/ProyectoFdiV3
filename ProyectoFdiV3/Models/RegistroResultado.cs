using System.ComponentModel.DataAnnotations;

namespace ProyectoFdiV3.Models
{
    public class RegistroResultado
    {
        [Key]
        public int IdRegistroResultado { get; set; }

        

        public int? IdDep { get; set; }
        public int? IdCom { get; set; }
        public int? IdMod { get; set; }
        public float? Tiempo1 { get; set; }
        public float? Tiempo2 { get; set; }


        public float? MaxEscala1 { get; set; }
        public float? MaxEscala2 { get; set; }
        
        public string? LabelMaxEscala1 { get; set; }
        public string? LabelMaxEscala2 { get; set; }
        

   
        public int TopB1 { get; set; }
        public int TopB2 { get; set; }
        public int TopB3 { get; set; }
        public int TopB4 { get; set; }

        public int ZonaB1 { get; set; }
        public int ZonaB2 { get; set; }
        public int ZonaB3 { get; set; }
        public int ZonaB4 { get; set; }

        public int ZonaA1 { get; set; }
        public int ZonaA2 { get; set; }
        public int ZonaA3 { get; set; }
        public int ZonaA4 { get; set; }



        //public int? Puesto { get; set; }
        public int? Orden { get; set; }
        public int? TipoRegistro { get; set; }

        public int? Etapa { get; set; }

        public bool RegistroCompleto { get; set; }

        public virtual Deportistum? Deportista { get; set; }
        public virtual Competencium? Competencia { get; set; }
        public int TotalTops { get; internal set; }
        public int TotalZonas { get; internal set; }
        public int TotalZonasL { get; internal set; }
        public int IntentosTops { get; internal set; }
        public int IntentosZonas { get; internal set; }
        public int IntentosZonasL { get; internal set; }

        public int? RankingVia1 { get; set; }
        public int? RankingVia2 { get; set; }
        public double PuntajeFinalVia { get; set; }

        public int? MaxPresas { get; set; }
        public double PuntajeCombinadaVia { get; set; }
        public double PuntajeCombinadaBloque { get; set; }

        public bool RegistroEditadoT1 { get; set; }
        public bool RegistroEditadoT2 { get; set; }

        public bool FallRegistro1 { get; set; }
        public bool FallRegistro2 { get; set; }

        public bool SalidaFalse { get; set; }

    }
}
