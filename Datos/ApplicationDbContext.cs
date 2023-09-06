using Microsoft.EntityFrameworkCore;
using Practica_API.Modelos;

namespace Practica_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Practica> Practicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Practica>().HasData(
                new Practica()
                {
                    Id = 1,
                    Nombre = "Practica uno",
                    Detalle = "Detalle de Practica uno",
                    ImagenUrl = "",
                    Ocupantes = 10,
                    Espacio = 10,
                    Tarifa = 10,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Practica()
                {
                    Id = 2,
                    Nombre = "Practica dos",
                    Detalle = "Detalle de Practica dos",
                    ImagenUrl = "",
                    Ocupantes = 20,
                    Espacio = 20,
                    Tarifa = 20,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );
        }
    }
}
