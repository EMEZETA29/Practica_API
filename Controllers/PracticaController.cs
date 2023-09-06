using Practica_API.Modelos;
using Practica_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticaController : ControllerBase
    {
        private readonly ILogger<PracticaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PracticaController(ILogger<PracticaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<PracticaDto>>> GetPracticas()
        {
            _logger.LogInformation("Obtener registros");

            IEnumerable<Practica> practicaList = await _db.Practicas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PracticaDto>>(practicaList));
            
        }

        [HttpGet("id:int", Name ="GetPractica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PracticaDto>> GetPractica(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer registro con Id" + id);
                return BadRequest();
            }
            //var practica = PracticaStore.practicaList.FirstOrDefault(p=> p.Id==id);
            var practica = await _db.Practicas.FirstOrDefaultAsync(p=> p.Id == id);

            if (practica == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PracticaDto>(practica));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PracticaDto>> CrearPractica([FromBody] PracticaCreateDto createDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Practicas.FirstOrDefaultAsync(p => p.Nombre.ToLower() == createDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "Este nombre ya existe");
                return BadRequest(ModelState);            
            }

            if(createDto == null)
            {
                return BadRequest(createDto);
            }

            Practica modelo = _mapper.Map<Practica>(createDto);

            await _db.Practicas.AddAsync(modelo);
            await _db.SaveChangesAsync();

            
            return CreatedAtRoute("GetPractica", new {id=modelo.Id}, modelo);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePractica(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var practica = await _db.Practicas.FirstOrDefaultAsync(p=>p.Id==id);
            if(practica == null)
            {
                return NotFound();
            }
            _db.Practicas.Remove(practica);
            await _db.SaveChangesAsync();

            return NoContent();
        
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePractica(int id, [FromBody] PracticaUpdateDto updateDto)
        {
            if(updateDto== null || id!= updateDto.Id)
            {
                return BadRequest();
            }


            Practica modelo = _mapper.Map<Practica>(updateDto);
            

            _db.Practicas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialPractica(int id, JsonPatchDocument<PracticaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var practica = await _db.Practicas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            PracticaUpdateDto practicaDto = _mapper.Map<PracticaUpdateDto>(practica);

            if (practica == null) return BadRequest();

            patchDto.ApplyTo(practicaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Practica modelo = _mapper.Map<Practica>(practicaDto);

            _db.Practicas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();

        }

    }
}
