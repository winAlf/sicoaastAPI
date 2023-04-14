using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Departamento;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Controllers
{
    [Route("api/Departamentos")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public DepartamentosController(IDepartamentoRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDepartamentos()
        {
            var listaDepartamentos = _ctRepo.GetDepartamentos();
            var ListaDepartamentosDto = new List<DepartamentoDto>();
            foreach (var lista in listaDepartamentos)
            {
                ListaDepartamentosDto.Add(_mapper.Map<DepartamentoDto>(lista));
            }
            return Ok(ListaDepartamentosDto);
        }

        [HttpGet("DepartamentoId:int", Name = "GetDepartamento")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDepartamento(int departamentoId)
        {
            var itemDepartamento = _ctRepo.GetDepartamento(departamentoId);

            if (itemDepartamento == null)
            {
                return NotFound();
            }

            var itemDepartamentoDto = _mapper.Map<DepartamentoDto>(itemDepartamento);
            return Ok(itemDepartamentoDto);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearDepartamento([FromBody] CrearDepartamentoDto crearDepartamentoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearDepartamentoDto == null)
            {
                return BadRequest(ModelState);
            }
            //if (_ctRepo.ExisteDepartamento(crearDepartamentoDto.Name))
            //{
            //    ModelState.AddModelError(" ", $"El Centro de Costo ya existe");
            //    return StatusCode(404, ModelState);
            //}
            var departamento = _mapper.Map<Departamento>(crearDepartamentoDto);
            if (!_ctRepo.CrearDepartamento(departamento))
            {
                ModelState.AddModelError(" ", $"Algo salió mal al guardar el registro {departamento.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetDepartamento", new { departamentoId = departamento.Id }, departamento);
        }

        [HttpPatch("{departamentoId}", Name = "ActualizarPatchDepartamento")]
        //[ProducesResponseType(201, Type=typeof(EmpresaDto))]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchDepartamento(int departamentoId, [FromBody] DepartamentoDto departamentoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (departamentoDto == null || departamentoId != departamentoDto.Id)
            {
                return BadRequest(ModelState);
            }
            var departamento = _mapper.Map<Departamento>(departamentoDto);
            if (!_ctRepo.ActualizarDepartamento(departamento))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {departamento.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{departamentoId:int}", Name = "BorrarDepartamento")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarDepartamento(int departamentoId)
        {
            if (!_ctRepo.ExisteDepartamento(departamentoId))
            {
                ModelState.AddModelError("", $"El registro numero {departamentoId} no existe, no se ha borrado nada");
                return StatusCode(400, ModelState);
            }
            var Departamento = _ctRepo.GetDepartamento(departamentoId);
            if (!_ctRepo.BorrarDepartamento(Departamento))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {Departamento.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetDepartamentosEnOrganismo/{ccostoId:int}")]
        public IActionResult GetDepartamentoEnOrganismo(int ccostoId)
        {
            var listaDepartamentos = _ctRepo.GetDepartamentoEnCcosto(ccostoId);
            if (listaDepartamentos == null)
            {
                return NotFound();
            }
            var itemDepartamento = new List<DepartamentoDto>();
            foreach(var item in listaDepartamentos)
            {
                itemDepartamento.Add(_mapper.Map<DepartamentoDto>(item));
            }
            return Ok(itemDepartamento);
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar(string name)
        {
            try
            {
                var resultado = _ctRepo.BuscarDepartamento(name.Trim());
                if(resultado.Any())
                {
                    //return Ok(resultado);
                    var itemDepartamento = new List<DepartamentoDto>();
                    foreach (var item in resultado)
                    {
                        itemDepartamento.Add(_mapper.Map<DepartamentoDto>(item));
                    }
                    return Ok(itemDepartamento);
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
