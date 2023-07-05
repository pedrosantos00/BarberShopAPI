using BarberShopAPI.BusinessLogic;
using BarberShopAPI.DAL;
using Microsoft.EntityFrameworkCore;

namespace BarberShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Configure CORS policy
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Policy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            // Configure DbContext
            builder.Services.AddDbContext<BarberShopDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerCon"));
                option.UseSqlServer(b => b.MigrationsAssembly("BarberShopAPI"));
            });

            // Register repositories
            builder.Services.AddScoped<ReservationRepository>();

            // Register services
            builder.Services.AddScoped<ReservationService>();

            // Register DbContext
            builder.Services.AddScoped<BarberShopDbContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("Policy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}