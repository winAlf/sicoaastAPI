using Microsoft.IdentityModel.Tokens;
using sicoaastAPI.Data;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Usuario;
using sicoaastAPI.Repository.IRepository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using XAct.Library.Settings;

namespace sicoaastAPI.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;
        private string claveSecreta;
        public UsuarioRepositorio(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }

        public Usuario GetUsuario(int UsuarioId)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == UsuarioId && u.activo == 1);
        }
        public ICollection<Usuario> GetUsuarios()
        {
            return _context.Usuarios.Where(u => u.activo == 1).OrderBy(u => u.NombreUsuario).ToList();
        }
        public bool IsUniqueUser(string usuario)
        {
            var usuariobd = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario);
            if(usuariobd == null)
            {
                return true;
            }
            return false;
        }
        public async Task<Usuario> Registro(CrearUsuarioDto crearUsuarioDto)
        {
            var passwordEncriptado = obtenermd5(crearUsuarioDto.Password);

            Usuario usuario = new Usuario
            {
                NombreUsuario = crearUsuarioDto.NombreUsuario,
                Password = passwordEncriptado,
                Nombre = crearUsuarioDto.Nombre,
                Role = crearUsuarioDto.Role,
                FechaCreacion = DateTime.Now,
                activo = 1
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return usuario;
        }
        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Password);

            var usuario = _context.Usuarios.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario.ToLower() 
                && u.Password == passwordEncriptado
            );
            
            //validamos si el usuario no existe con la combinacion de usuario y contraseña correctas
            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            //si existe el usuario
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token=manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRespuestaDto;
        }
        public bool ActualizarUsuario(Usuario usuario)
        {
            usuario.FechaActualizacion = DateTime.Now;
            _context.Usuarios.Update(usuario);
            return Guardar();
        }
        public bool BorrarUsuario(Usuario usuario)
        {
            usuario.activo = 0;
            usuario.FechaBaja = DateTime.Now;
            _context.Usuarios.Update(usuario);
            return Guardar();
        }
        public bool ReactivarUsuario(Usuario usuario)
        {
            usuario.activo = 1;
            usuario.FechaReactivacionn = DateTime.Now;
            _context.Usuarios.Update(usuario);
            return Guardar();
        }
        public ICollection<Usuario> BuscarUsuario(string textoBuscar)
        {
            IQueryable<Usuario> query = _context.Usuarios;
            if (!string.IsNullOrEmpty(textoBuscar))
            {
                query = query.Where(e => e.NombreUsuario.Contains(textoBuscar) || e.Nombre.Contains(textoBuscar) && e.activo == 1);
                //query = query.Where(e => e.NombreCompleto.Contains(nombre) && e.activo == 1);
            }
            return query.ToList();
        }
        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
        public static string obtenermd5(string password)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
        //public Usuario GetUsuarioActivo(int UsuarioId)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool CrearUsuario(Usuario usuario)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool ExisteUsuario(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool ExisteUsuario(string usuario)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
