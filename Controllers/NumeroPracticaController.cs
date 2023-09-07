using Practica_API.Modelos;
using Practica_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Practica_API.Repositorio.IRepositorio;
using System.Net;

namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroPracticaController : ControllerBase
    {
        private readonly ILogger<NumeroPracticaController> _logger;
        private readonly IPracticaRepositorio _practicaRepo;
        private readonly INumeroPracticaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroPracticaController(ILogger<NumeroPracticaController> logger, IPracticaRepositorio practicaRepo, INumeroPracticaRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _practicaRepo = practicaRepo;
            _mapper = mapper;
            _response = new();
            _numeroRepo = numeroRepo;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetNumeroPracticas()
        {
            try
            {
                _logger.LogInformation("Obtener registros");

                IEnumerable<NumeroPractica> numeroPracticaList = await _numeroRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroPracticaDto>>(numeroPracticaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
            
        }

        [HttpGet("id:int", Name ="GetNumeroPractica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroPractica(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero registro con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                //var practica = PracticaStore.practicaList.FirstOrDefault(p=> p.Id==id);
                var numeroPractica = await _numeroRepo.Obtener(p => p.PracticaNo == id);

                if (numeroPractica == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroPracticaDto>(numeroPractica);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroPractica([FromBody] NumeroPracticaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroRepo.Obtener(p => p.PracticaNo == createDto.PracticaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "Este numero ya existe");
                    return BadRequest(ModelState);
                }

                if (await _practicaRepo.Obtener(p=>p.Id==createDto.PracticaId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "Este Id no existe");
                    return BadRequest(ModelState);
                }

                NumeroPractica modelo = _mapper.Map<NumeroPractica>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetPractica", new { id = modelo.PracticaNo }, _response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
            

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroPractica(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numeroPractica = await _numeroRepo.Obtener(p => p.PracticaNo == id);
                if (numeroPractica == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numeroRepo.Remover(numeroPractica);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response); 
            
        
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroPractica(int id, [FromBody] NumeroPracticaUpdateDto updateDto)
        {
            if(updateDto== null || id!= updateDto.PracticaNo)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }

            if(await _practicaRepo.Obtener(p=>p.Id == updateDto.PracticaId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de no existe!");
                return BadRequest(ModelState);
            }

            NumeroPractica modelo = _mapper.Map<NumeroPractica>(updateDto);
            

            await _numeroRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }



    }
}
