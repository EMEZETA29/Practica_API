using Practica_API.Modelos;
using Practica_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Practica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticaController : ControllerBase
    {
        private readonly ILogger<PracticaController> _logger;
        private readonly ApplicationDbContext _db;

        public PracticaController(ILogger<PracticaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PracticaDto>> GetPracticas()
        {
            _logger.LogInformation("Obtener registros");
            return Ok(_db.Practicas.ToList());
            
        }

        [HttpGet("id:int", Name ="GetPractica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PracticaDto> GetPractica(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer registro con Id" + id);
                return BadRequest();
            }
            //var practica = PracticaStore.practicaList.FirstOrDefault(p=> p.Id==id);
            var practica = _db.Practicas.FirstOrDefault(p=> p.Id == id);

            if (practica == null)
            {
                return NotFound();
            }

            return Ok(practica);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PracticaDto> CrearPractica([FromBody] PracticaDto practicaDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Practicas.FirstOrDefault(p => p.Nombre.ToLower() == practicaDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "Este nombre ya existe");
                return BadRequest(ModelState);            
            }

            if(practicaDto ==null)
            {
                return BadRequest(practicaDto);
            }
            if(practicaDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Practica modelo = new()
            {
                Nombre = practicaDto.Nombre,
                Detalle = practicaDto.Detalle,
                ImagenUrl = practicaDto.ImagenUrl,
                Ocupantes = practicaDto.Ocupantes,
                Tarifa = practicaDto.Tarifa,
                Espacio = practicaDto.Espacio,
                Amenidad = practicaDto.Amenidad
            };

            _db.Practicas.Add(modelo);
            _db.SaveChanges();

            
            return CreatedAtRoute("GetPractica", new {id=practicaDto.Id}, practicaDto);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePractica(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var practica = _db.Practicas.FirstOrDefault(p=>p.Id==id);
            if(practica == null)
            {
                return NotFound();
            }
            _db.Practicas.Remove(practica);
            _db.SaveChanges();

            return NoContent();
        
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePractica(int id, [FromBody] PracticaDto practicaDto)
        {
            if(practicaDto== null || id!= practicaDto.Id)
            {
                return BadRequest();
            }
            //var practica = PracticaStore.practicaList.FirstOrDefault(p=>p.Id==id);
            //practica.Nombre = practicaDto.Nombre;
            //practica.Ocupantes = practicaDto.Ocupantes;
            //practica.Espacio = practicaDto.Espacio;

            Practica modelo = new()
            {
                Id = practicaDto.Id,
                Nombre = practicaDto.Nombre,
                Detalle = practicaDto.Detalle,
                ImagenUrl = practicaDto.ImagenUrl,
                Ocupantes = practicaDto.Ocupantes,
                Tarifa = practicaDto.Tarifa,
                Espacio = practicaDto.Espacio,
                Amenidad = practicaDto.Amenidad
            };

            _db.Practicas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialPractica(int id, JsonPatchDocument<PracticaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var practica = _db.Practicas.AsNoTracking().FirstOrDefault(p => p.Id == id);

            PracticaDto practicaDto = new()
            {
                Id = practica.Id,
                Nombre = practica.Nombre,
                Detalle = practica.Detalle,
                ImagenUrl = practica.ImagenUrl,
                Ocupantes = practica.Ocupantes,
                Tarifa = practica.Tarifa,
                Espacio = practica.Espacio,
                Amenidad = practica.Amenidad
            };

            if (practica == null) return BadRequest();

            patchDto.ApplyTo(practicaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Practica modelo = new()
            {
                Id = practicaDto.Id,
                Nombre = practicaDto.Nombre,
                Detalle = practicaDto.Detalle,
                ImagenUrl = practicaDto.ImagenUrl,
                Ocupantes = practicaDto.Ocupantes,
                Tarifa = practicaDto.Tarifa,
                Espacio = practicaDto.Espacio,
                Amenidad = practicaDto.Amenidad
            };

            _db.Practicas.Update(modelo);
            _db.SaveChanges();
            return NoContent();

        }

    }
}
