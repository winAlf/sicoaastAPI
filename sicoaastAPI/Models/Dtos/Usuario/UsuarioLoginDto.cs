using System.ComponentModel.DataAnnotations;

namespace sicoaastAPI.Models.Dtos.Usuario
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El nombre de usaurio es obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
