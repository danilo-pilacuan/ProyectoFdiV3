namespace ProyectoFDI.API.V3.Models
{
    public class RegistroResultado
    {
        public int IdRegistroResultado { get; set; }

        public int? IdCom { get; set; }

        public int? IdDep { get; set; }

        public int? Puesto { get; set; }

        public string? Etapa { get; set; }

        public virtual Competencium? IdComNavigation { get; set; }

        public virtual Deportistum? IdDepNavigation { get; set; }

    }
}
