using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Ccosto;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Controllers
{
    [Route("api/Ccostos")]
    [ApiController]
    public class CcostosController : ControllerBase
    {
        private readonly ICcostoRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public CcostosController(ICcostoRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCcostos()
        {
            var listaCcostos = _ctRepo.GetCcostos();
            var ListaCcostosDto = new List<CcostoDto>();
            foreach (var lista in listaCcostos)
            {
                ListaCcostosDto.Add(_mapper.Map<CcostoDto>(lista));
            }
            return Ok(ListaCcostosDto);
        }

        [HttpGet("CcostoId:int", Name = "GetCcosto")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCcosto(int ccostoId)
        {
            var itemCcosto = _ctRepo.GetCcosto(ccostoId);

            if (itemCcosto == null)
            {
                return NotFound();
            }

            var itemCcostoDto = _mapper.Map<CcostoDto>(itemCcosto);
            return Ok(itemCcostoDto);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCcosto([FromBody] CrearCcostoDto crearCcostoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearCcostoDto == null)
            {
                return BadRequest(ModelState);
            }
            //if (_ctRepo.ExisteCcosto(crearCcostoDto.Name))
            //{
            //    ModelState.AddModelError(" ", $"El Centro de Costo ya existe");
            //    return StatusCode(404, ModelState);
            //}
            var ccosto = _mapper.Map<Ccosto>(crearCcostoDto);
            if (!_ctRepo.CrearCcosto(ccosto))
            {
                ModelState.AddModelError(" ", $"Algo salió mal al guardar el registro {ccosto.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCcosto", new { ccostoId = ccosto.Id }, ccosto);
        }

        [HttpPatch("{ccostoId}", Name = "ActualizarPatchCcosto")]
        //[ProducesResponseType(201, Type=typeof(EmpresaDto))]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchCcosto(int ccostoId, [FromBody] CcostoDto ccostoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ccostoDto == null || ccostoId != ccostoDto.Id)
            {
                return BadRequest(ModelState);
            }
            var ccosto = _mapper.Map<Ccosto>(ccostoDto);
            if (!_ctRepo.ActualizarCcosto(ccosto))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {ccosto.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ccostoId:int}", Name = "BorrarCcosto")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarCcosto(int ccostoId)
        {
            if (!_ctRepo.ExisteCcosto(ccostoId))
            {
                ModelState.AddModelError("", $"El registro numero {ccostoId} no existe, no se ha borrado nada");
                return StatusCode(400, ModelState);
            }
            var ccosto = _ctRepo.GetCcosto(ccostoId);
            if (!_ctRepo.BorrarCcosto(ccosto))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {ccosto.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetCcostosEnOrganismo/{organismoId:int}")]
        public IActionResult GetCcostoEnOrganismo(int organismoId)
        {
            var listaCcostos = _ctRepo.GetCcostoEnOrganismo(organismoId);
            if (listaCcostos == null)
            {
                return NotFound();
            }
            var itemCcosto = new List<CcostoDto>();
            foreach(var item in listaCcostos)
            {
                itemCcosto.Add(_mapper.Map<CcostoDto>(item));
            }
            return Ok(itemCcosto);
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar(string name)
        {
            try
            {
                var resultado = _ctRepo.BuscarCcosto(name.Trim());
                if(resultado.Any())
                {
                    //return Ok(resultado);
                    var itemCcosto = new List<CcostoDto>();
                    foreach (var item in resultado)
                    {
                        itemCcosto.Add(_mapper.Map<CcostoDto>(item));
                    }
                    return Ok(itemCcosto);
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
