using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Application.Service;
using Sistema_Citas.Intrafestructura.Sistema_Citas.Repository;
using Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext;

namespace Sistema_Citas.Intrafestructura.Sistema_Citas.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // 1. Add DbContext (siempre primero)
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. Add Generic Repositories (base para todos los repositorios)
            services.AddScoped(typeof(IGeneryRepository<>), typeof(GeneryRepository<>));

            // 3. Add Specific Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEstacionRepository, EstacionRepository>();
            services.AddScoped<ICitasRepository, CitaRepository>();

            // 4. Add Generic Services (base para todos los servicios)
            services.AddScoped(typeof(IGeneryService<>), typeof(GeneryService<>));

            // 5. Add Specific Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEstacionService, EstacionService>(); 
            services.AddScoped<ICitasService, CitasService>();
            return services;
        }
    }
}
