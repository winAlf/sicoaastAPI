using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.MovEmp;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Controllers
{
    [Route("api/MovEmp")]
    [ApiController]
    public class MovEmpController : ControllerBase
    {
        private readonly IMovEmpRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public MovEmpController(IMovEmpRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovEmps()
        {
            var listaMovEmps = _ctRepo.GetMovEmps();
            var ListaMovEmpsDto = new List<MovEmpDto>();
            foreach (var lista in listaMovEmps)
            {
                ListaMovEmpsDto.Add(_mapper.Map<MovEmpDto>(lista));
            }
            return Ok(ListaMovEmpsDto);
        }

        [HttpGet("MovEmpId:int", Name = "GetMovEmp")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMovEmp(int movEmpId)
        {
            var itemMovEmp = _ctRepo.GetMovEmp(movEmpId);

            if (itemMovEmp == null)
            {
                return NotFound();
            }

            var itemMovEmpDto = _mapper.Map<MovEmpDto>(itemMovEmp);
            return Ok(itemMovEmpDto);
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearMovEmp([FromBody] CrearMovEmpDto crearMovEmpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearMovEmpDto == null)
            {
                return BadRequest(ModelState);
            }
            //if (_ctRepo.ExisteMovEmp(crearMovEmpDto.Name))
            //{
            //    ModelState.AddModelError(" ", $"El Centro de Costo ya existe");
            //    return StatusCode(404, ModelState);
            //}
            var movEmp = _mapper.Map<MovEmp>(crearMovEmpDto);
            if (!_ctRepo.CrearMovEmp(movEmp))
            {
                ModelState.AddModelError(" ", $"Algo salió mal al guardar el registro {movEmp.Nombre} {movEmp.Apaterno} {movEmp.Amaterno}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetMovEmp", new { movEmpId = movEmp.Id }, movEmp);
        }

        [HttpPatch("{movEmpId}", Name = "ActualizarPatchMovEmp")]
        //[ProducesResponseType(201, Type=typeof(EmpresaDto))]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchMovEmp(int movEmpId, [FromBody] PatchMovEmpDto patchMovEmpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (patchMovEmpDto == null || movEmpId != patchMovEmpDto.Id)
            {
                return BadRequest(ModelState);
            }
            var movEmp = _mapper.Map<MovEmp>(patchMovEmpDto);
            if (!_ctRepo.ActualizarMovEmp(movEmp))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {movEmp.Nombre} {movEmp.Apaterno} {movEmp.Amaterno}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPatch("delete/{movEmpId:int}", Name = "BorrarMovEmp")]
        //[Route("api/MovEmp/delete")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarMovEmp(int movEmpId)
        {
            if (!_ctRepo.ExisteMovEmp(movEmpId))
            {
                ModelState.AddModelError("", $"El registro numero {movEmpId} no existe, no se ha borrado nada");
                return StatusCode(400, ModelState);
            }
            var MovEmp = _ctRepo.GetMovEmpActivo(movEmpId);
            if (!_ctRepo.BorrarMovEmp(MovEmp))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro numero {movEmpId}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPatch("reactivar/{movEmpId:int}", Name = "ReactivarMovEmp")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ReactivarMovEmp(int movEmpId)
        {
            if (!_ctRepo.ExisteMovEmp(movEmpId))
            {
                ModelState.AddModelError("", $"El registro numero {movEmpId} no existe, no se ha reactivado nada");
                return StatusCode(400, ModelState);
            }
            var MovEmp = _ctRepo.GetMovEmpActivo(movEmpId);
            if (!_ctRepo.ReactivarMovEmp(MovEmp))
            {
                ModelState.AddModelError("", $"Algo salió mal reactivando el registro numero {movEmpId}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetMovEmpsEnEmpresa/{empresaId:int}")]
        public IActionResult GetMovEmpEnEmpresa(int empresaId)
        {
            var listaMovEmps = _ctRepo.GetMovEmpEnEmpresa(empresaId);
            if (listaMovEmps == null)
            {
                return NotFound();
            }
            var itemMovEmp = new List<MovEmpDto>();
            foreach (var item in listaMovEmps)
            {
                itemMovEmp.Add(_mapper.Map<MovEmpDto>(item));
            }
            return Ok(itemMovEmp);
        }

        [HttpGet("GetMovEmpsEnOrganismo/{organismoId:int}")]
        public IActionResult GetMovEmpEnOrganismo(int organismoId)
        {
            var listaMovEmps = _ctRepo.GetMovEmpEnOrganismo(organismoId);
            if (listaMovEmps == null)
            {
                return NotFound();
            }
            var itemMovEmp = new List<MovEmpDto>();
            foreach (var item in listaMovEmps)
            {
                itemMovEmp.Add(_mapper.Map<MovEmpDto>(item));
            }
            return Ok(itemMovEmp);
        }

        [HttpGet("GetMovEmpsEnCcosto/{ccostoId:int}")]
        public IActionResult GetMovEmpEnCcosto(int ccostoId)
        {
            var listaMovEmps = _ctRepo.GetMovEmpEnCcosto(ccostoId);
            if (listaMovEmps == null)
            {
                return NotFound();
            }
            var itemMovEmp = new List<MovEmpDto>();
            foreach (var item in listaMovEmps)
            {
                itemMovEmp.Add(_mapper.Map<MovEmpDto>(item));
            }
            return Ok(itemMovEmp);
        }

        [HttpGet("GetMovEmpsEnDepartamento/{departamentoId:int}")]
        public IActionResult GetMovEmpEnDepartamento(int departamentoId)
        {
            var listaMovEmps = _ctRepo.GetMovEmpEnDepartamento(departamentoId);
            if (listaMovEmps == null)
            {
                return NotFound();
            }
            var itemMovEmp = new List<MovEmpDto>();
            foreach (var item in listaMovEmps)
            {
                itemMovEmp.Add(_mapper.Map<MovEmpDto>(item));
            }
            return Ok(itemMovEmp);
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar(string name)
        {
            try
            {
                var resultado = _ctRepo.BuscarMovEmp(name.Trim());
                if (resultado.Any())
                {
                    //return Ok(resultado);
                    var itemMovEmp = new List<MovEmpDto>();
                    foreach (var item in resultado)
                    {
                        itemMovEmp.Add(_mapper.Map<MovEmpDto>(item));
                    }
                    return Ok(itemMovEmp);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }
        }
    }
}
