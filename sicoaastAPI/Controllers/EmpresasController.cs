using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Empresa;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public EmpresasController(IEmpresaRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo= ctRepo;
            _mapper= mapper;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEmpresas()
        {
            var listaEmpresas = _ctRepo.GetEmpresas();
            var ListaEmpresasDto = new List<EmpresaDto>();
            foreach(var lista in listaEmpresas)
            {
                ListaEmpresasDto.Add(_mapper.Map<EmpresaDto>(lista));
            }
            return Ok(ListaEmpresasDto);
        }

        [HttpGet("EmpresaId:int", Name ="GetEmpresa")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getEmpresa(int empresaId)
        {
            var itemEmpresa = _ctRepo.GetEmpresa(empresaId);

            if (itemEmpresa == null)
            {
                return NotFound();
            }

            var itemEmpresaDto = _mapper.Map<EmpresaDto>(itemEmpresa);
            return Ok(itemEmpresaDto);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearEmpresa([FromBody] CrearEmpresaDto crearEmpresaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearEmpresaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_ctRepo.ExisteEmpresa(crearEmpresaDto.Name))
            {
                ModelState.AddModelError(" ", $"La empresa ya existe");
                return StatusCode(404, ModelState);
            }
            var empresa = _mapper.Map<Empresa>(crearEmpresaDto);
            if (!_ctRepo.CrearEmpresa(empresa))
            {
                ModelState.AddModelError(" ", $"Algo salió mal al guardar el registro {empresa.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetEmpresa", new {empresaId=empresa.Id}, empresa);
        }

        [HttpPatch("{empresaId}", Name = "ActualizarPatchEmpresa")]
        //[ProducesResponseType(201, Type=typeof(EmpresaDto))]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchEmpresa(int empresaId, [FromBody] EmpresaDto empresaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(empresaDto == null || empresaId != empresaDto.Id)
            {
                return BadRequest(ModelState); 
            }
            var empresa = _mapper.Map<Empresa>(empresaDto);
            if (!_ctRepo.ActualizarEmpresa(empresa))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {empresa.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{empresaId:int}", Name="BorrarEmpresa")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarEmpresa (int empresaId)
        {
            if (!_ctRepo.ExisteEmpresa(empresaId))
            {
                ModelState.AddModelError("", $"El registro numero {empresaId} no existe, no se ha borrado nada");
                return StatusCode(400, ModelState);
            }
            var empresa = _ctRepo.GetEmpresa(empresaId);
            if (!_ctRepo.BorrarEmpresa(empresa))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {empresa.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
