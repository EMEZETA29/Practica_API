using Practica_API.Modelos.Dto;

namespace Practica_API.Datos
{
    public static class PracticaStore
    {
        public static List<PracticaDto> practicaList = new List<PracticaDto>
        {
            new PracticaDto{Id=1, Nombre="Max Poblete Cortes", Ocupantes=3, Espacio=50},
            new PracticaDto{Id=2, Nombre="Liza Gualter Paredes", Ocupantes=4, Espacio=70}
        };
    }
}
