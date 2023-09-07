using Practica_API.Datos;
using Practica_API.Modelos;
using Practica_API.Repositorio.IRepositorio;

namespace Practica_API.Repositorio
{
    public class NumeroPracticaRepositorio : Repositorio<NumeroPractica>, INumeroPracticaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public NumeroPracticaRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<NumeroPractica> Actualizar(NumeroPractica entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroPracticas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
