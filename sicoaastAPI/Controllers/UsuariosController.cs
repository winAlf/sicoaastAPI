using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Usuario;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        protected RespuestaAPI _respuestaAPI;
        private readonly IUsuarioRepositorio _uRepo;
        private readonly IMapper _mapper;
        public UsuariosController(IUsuarioRepositorio uRepo, IMapper mapper)
        {
            _uRepo = uRepo;
            this._respuestaAPI = new();
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _uRepo.GetUsuarios();
            var listaUsuariosDto = new List<UsuarioDto>();

            foreach(var lista in  listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }
            return Ok(listaUsuariosDto);
        }

        [HttpGet ("{usuarioId:int}",Name ="GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario=_uRepo.GetUsuario(usuarioId);
            if(itemUsuario== null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        [HttpPost ("registo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] CrearUsuarioDto crearUsuarioDto)
        {
            bool validarNombreUsuarioUnico = _uRepo.IsUniqueUser(crearUsuarioDto.NombreUsuario);
            if (!validarNombreUsuarioUnico)
            {
                _respuestaAPI.StatusCode=System.Net.HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess=false;
                _respuestaAPI.ErrorMessages.Add("El nombre de usuario ya existe...");
                return BadRequest(_respuestaAPI);
            }

            var usuario = await _uRepo.Registro(crearUsuarioDto);
            if (usuario == null)
            {
                _respuestaAPI.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess = false;
                _respuestaAPI.ErrorMessages.Add("Error en el registro");
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.StatusCode = System.Net.HttpStatusCode.OK;
            _respuestaAPI.IsSuccess = true;
            return Ok(_respuestaAPI);
        }

        [HttpPost ("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            var respuestaLogin = await _uRepo.Login(loginDto);
            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaAPI.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess = false;
                _respuestaAPI.ErrorMessages.Add("El nombre de usuario u/o password son incorrectos");
                return BadRequest(_respuestaAPI);
            }
            _respuestaAPI.StatusCode = System.Net.HttpStatusCode.OK;
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = respuestaLogin;
            return Ok(_respuestaAPI);
        }
    }
}
