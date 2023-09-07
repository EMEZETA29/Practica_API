using Practica_API.Modelos;

namespace Practica_API.Repositorio.IRepositorio
{
    public interface IPracticaRepositorio :IRepositorio<Practica>

    {
        Task<Practica> Actualizar(Practica entidad);
    }
}
