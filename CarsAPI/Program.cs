
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = "Server=Emil\\SQLEXPRESS;Database=CarsAPI;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

            builder.Services.AddControllers();
            builder.Services.AddDbContext<CarsContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
