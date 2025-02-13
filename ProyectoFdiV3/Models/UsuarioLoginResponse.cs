namespace ProyectoFDI.API.V3.Models
{
    public class UsuarioLoginResponse
    {
        public int IdUsu { get; set; }

        public string? NombreUsu { get; set; }

        public string? TokenUsu { get; set; }

        //public DateTime? FechaCreacion { get; set; }

        public string? RolesUsu { get; set; }

        public bool? ActivoUsu { get; set; }
    }
}
