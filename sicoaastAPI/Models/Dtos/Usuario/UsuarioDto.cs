﻿namespace sicoaastAPI.Models.Dtos.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
