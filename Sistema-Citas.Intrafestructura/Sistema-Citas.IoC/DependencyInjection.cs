using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext;

namespace Sistema_Citas.Intrafestructura.Sistema_Citas.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            //Configuración de los repositorios
            //            // Configuración de la cadena de conexión a la base de datos
            //services.AddDbContext < DBContext(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //services.AddScoped<IEstacionRepository, EstacionRepository>();
            //services.AddScoped<ICitaRepository, CitaRepository>();
            //// Configuración de servicios adicionales
            //services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
