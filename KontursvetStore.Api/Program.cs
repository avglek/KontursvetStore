using KontursvetStore.Application.Services;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.DataAccess;
using KontursvetStore.DataAccess.Repositories;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace KontursvetStore.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // получаем строку подключения из файла конфигурации
        //builder.Configuration.AddJsonFile("appsettings.local.json");
        
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        
        // добавляем контекст ApplicationContext в качестве сервиса в приложение
        builder.Services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(connection));
        
        // Configure CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Replace with your Angular app's URL
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });

        }
        
// Enable CORS middleware
        app.UseCors("AllowSpecificOrigin"); // Use the policy name defined above
        
//        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}