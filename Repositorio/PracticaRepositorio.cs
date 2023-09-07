using Practica_API.Datos;
using Practica_API.Modelos;
using Practica_API.Repositorio.IRepositorio;

namespace Practica_API.Repositorio
{
    public class PracticaRepositorio : Repositorio<Practica>, IPracticaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public PracticaRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<Practica> Actualizar(Practica entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Practicas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
