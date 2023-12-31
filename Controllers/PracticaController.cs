﻿using Practica_API.Modelos;
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
    public class PracticaController : ControllerBase
    {
        private readonly ILogger<PracticaController> _logger;
        private readonly IPracticaRepositorio _practicaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PracticaController(ILogger<PracticaController> logger, IPracticaRepositorio practicaRepo, IMapper mapper)
        {
            _logger = logger;
            _practicaRepo = practicaRepo;
            _mapper = mapper;
            _response = new();

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetPracticas()
        {
            try
            {
                _logger.LogInformation("Obtener registros");

                IEnumerable<Practica> practicaList = await _practicaRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<PracticaDto>>(practicaList);
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

        [HttpGet("id:int", Name ="GetPractica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPractica(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer registro con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                //var practica = PracticaStore.practicaList.FirstOrDefault(p=> p.Id==id);
                var practica = await _practicaRepo.Obtener(p => p.Id == id);

                if (practica == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<PracticaDto>(practica);
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
        public async Task<ActionResult<APIResponse>> CrearPractica([FromBody] PracticaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _practicaRepo.Obtener(p => p.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "Este nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Practica modelo = _mapper.Map<Practica>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _practicaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetPractica", new { id = modelo.Id }, _response);

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
        public async Task<IActionResult> DeletePractica(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var practica = await _practicaRepo.Obtener(p => p.Id == id);
                if (practica == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _practicaRepo.Remover(practica);

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
        public async Task<IActionResult> UpdatePractica(int id, [FromBody] PracticaUpdateDto updateDto)
        {
            if(updateDto== null || id!= updateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }


            Practica modelo = _mapper.Map<Practica>(updateDto);
            

            await _practicaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
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
            var practica = await _practicaRepo.Obtener(p => p.Id == id, tracked:false);

            PracticaUpdateDto practicaDto = _mapper.Map<PracticaUpdateDto>(practica);

            if (practica == null) return BadRequest();

            patchDto.ApplyTo(practicaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Practica modelo = _mapper.Map<Practica>(practicaDto);

            await _practicaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }

    }
}
