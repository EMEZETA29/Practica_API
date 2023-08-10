﻿using System.ComponentModel.DataAnnotations;

namespace Practica_API.Modelos.Dto
{
    public class PracticaCreateDto
    {

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public double Espacio { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set;}
    }
}
