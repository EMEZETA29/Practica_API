using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica_API.Modelos
{
    public class NumeroPractica
    {
       [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
       public int PracticaNo { get; set; }
       
       [Required]
       public int PracticaId { get; set; }

       [ForeignKey("PracticaId")]
       public Practica Practica { get; set; }

       public string DetalleEspecial { get; set; }
       public DateTime FechaCreacion { get; set; }
       public DateTime FechaActualizacion { get; set; }
    }
}
