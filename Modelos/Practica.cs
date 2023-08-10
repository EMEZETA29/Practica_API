using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica_API.Modelos
{
    public class Practica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public double Espacio { get; set; }
        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; } 


    }
}
