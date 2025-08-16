using Microsoft.EntityFrameworkCore;
using Sistema_Citas.Domain.Entity;


namespace Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
          public DbSet<Usuario> Usuario { get; set; }  
        public DbSet<Estacion> Estacion { get; set; }
        public DbSet<Cita> Cita { get; set; }

    }
}
