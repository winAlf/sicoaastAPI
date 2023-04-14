using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models
{
    public class Usuario
    {
        [Key] 
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaBaja { get; set; }
        public DateTime FechaReactivacionn { get; set; }


    }
}
