using BarberShopAPI.BusinessLogic;
using BarberShopAPI.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            builder.Services.AddScoped<BarberRepository>();
            builder.Services.AddScoped<AppointmentRepository>();
            builder.Services.AddScoped<ClientRepository>();

            // Register services
            builder.Services.AddScoped<BarberService>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<ClientService>();

            // Register DbContext
            builder.Services.AddScoped<BarberShopDbContext>();

            // Register JWT service
            builder.Services.AddScoped<JWTService>();


            // Configure authentication and JWT Bearer
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("NxgW6KsCk6O0XpQdvnuy16jfRk5ceag9ZhjgERymnhI=")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });



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