using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Usuario;

namespace sicoaastAPI.Repository.IRepository
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();

        Usuario GetUsuario(int UsuarioId);
        //Usuario GetUsuarioActivo(int UsuarioId);
        //bool ExisteUsuario (int id);

        //bool ExisteUsuario(string usuario);
        bool IsUniqueUser(string usuario);

        //bool CrearUsuario(Usuario usuario);

        bool ActualizarUsuario(Usuario usuario);

        bool BorrarUsuario(Usuario usuario);
        bool ReactivarUsuario(Usuario usuario);
        bool Guardar();

        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario> Registro(CrearUsuarioDto crearUsuarioDto);

        ICollection<Usuario> BuscarUsuario(string textoBuscar);
    }
}
