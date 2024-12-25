using Microsoft.Extensions.DependencyInjection;
using Cars.Data;
using Cars.Data.Services.Implementations;
using Cars.Data.Services.Interfaces;

namespace Cars.Web.Infrastructure.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add services to the container.
            string connectionString = "Server=Emil\\SQLEXPRESS;Database=CarsAPI;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

            services.AddControllers();
            services.AddDbContext<CarsContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IGarageService, GarageService>();
            services.AddScoped<ICarService, CarService>();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });
            return services;
        }
    }
}
