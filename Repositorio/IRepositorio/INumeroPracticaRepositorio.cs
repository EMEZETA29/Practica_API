using Practica_API.Modelos;

namespace Practica_API.Repositorio.IRepositorio
{
    public interface INumeroPracticaRepositorio :IRepositorio<NumeroPractica>

    {
        Task<NumeroPractica> Actualizar(NumeroPractica entidad);
    }
}
