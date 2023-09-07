using System.ComponentModel.DataAnnotations;

namespace Practica_API.Modelos.Dto
{
    public class NumeroPracticaCreateDto
    {
        [Required]
        public int PracticaNo { get; set; }

        [Required]
        public int PracticaId { get; set; }
        public string DetalleEspecial { get; set; }

    }
}
