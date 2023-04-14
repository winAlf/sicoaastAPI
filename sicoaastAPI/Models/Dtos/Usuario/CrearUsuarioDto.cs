using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models.Dtos.Usuario
{
    public class CrearUsuarioDto
    {
        [Required(ErrorMessage = "El nombre de usaurio es obligatorio")]
        [MaxLength(100, ErrorMessage = "El numero de caracteres del nombre de usuario, debe ser menor a 20")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El numero de caracteres del nombre, debe ser menor a 100")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
