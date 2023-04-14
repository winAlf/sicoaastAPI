using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Empresa;
using sicoaastAPI.Models.Dtos.Organismo;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Controllers
{
    [Route("api/Organismos")]
    [ApiController]
    public class OrganismosController : ControllerBase
    {
        private readonly IOrganismoRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public OrganismosController(IOrganismoRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrganismos()
        {
            var listaOrganismos = _ctRepo.GetOrganismos();
            var ListaOrganismosDto = new List<OrganismoDto>();
            foreach (var lista in listaOrganismos)
            {
                ListaOrganismosDto.Add(_mapper.Map<OrganismoDto>(lista));
            }
            return Ok(ListaOrganismosDto);
        }

        [HttpGet("OrganismoId:int", Name = "GetOrganismo")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getOrganismo(int organismoId)
        {
            var itemOrganismo = _ctRepo.GetOrganismo(organismoId);

            if (itemOrganismo == null)
            {
                return NotFound();
            }

            var itemOrganismoDto = _mapper.Map<OrganismoDto>(itemOrganismo);
            return Ok(itemOrganismoDto);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearOrganismo([FromBody] CrearOrganismoDto crearOrganismoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearOrganismoDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_ctRepo.ExisteOrganismo(crearOrganismoDto.Name))
            {
                ModelState.AddModelError(" ", $"El organismo ya existe");
                return StatusCode(404, ModelState);
            }
            var organismo = _mapper.Map<Organismo>(crearOrganismoDto);
            if (!_ctRepo.CrearOrganismo(organismo))
            {
                ModelState.AddModelError(" ", $"Algo salió mal al guardar el registro {organismo.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetOrganismo", new { organismoId = organismo.Id }, organismo);
        }

        [HttpPatch("{organismoId}", Name = "ActualizarPatchOrganismo")]
        //[ProducesResponseType(201, Type=typeof(EmpresaDto))]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchOrganismo(int organismoId, [FromBody] OrganismoDto organismoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (organismoDto == null || organismoId != organismoDto.Id)
            {
                return BadRequest(ModelState);
            }
            var organismo = _mapper.Map<Organismo>(organismoDto);
            if (!_ctRepo.ActualizarOrganismo(organismo))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {organismo.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{organismoId:int}", Name = "BorrarOrganismo")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarOrganismo(int organismoId)
        {
            if (!_ctRepo.ExisteOrganismo(organismoId))
            {
                ModelState.AddModelError("", $"El registro numero {organismoId} no existe, no se ha borrado nada");
                return StatusCode(400, ModelState);
            }
            var organismo = _ctRepo.GetOrganismo(organismoId);
            if (!_ctRepo.BorrarOrganismo(organismo))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {organismo.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetOrganismosEnEmpresa/{empresaId:int}")]
        public IActionResult GetOrganismoEnEmpresa(int empresaId)
        {
            var listaOrganismos = _ctRepo.GetOrganismoEnEmpresa(empresaId);
            if (listaOrganismos == null)
            {
                return NotFound();
            }
            var itemOrganismo = new List<OrganismoDto>();
            foreach(var item in listaOrganismos)
            {
                itemOrganismo.Add(_mapper.Map<OrganismoDto>(item));
            }
            return Ok(itemOrganismo);
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar(string name)
        {
            try
            {
                var resultado = _ctRepo.BuscarOrganismo(name.Trim());
                if(resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }
        }
    }
}
