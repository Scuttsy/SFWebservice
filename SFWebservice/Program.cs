using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SFWebservice
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddDbContext<Sfdb01Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Subtle Feedback",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapMethods("/_options-test", new[] { "OPTIONS" }, () =>
            {
                return Results.Ok("OPTIONS reached ASP.NET");
            });


            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.Request.Method == HttpMethods.Options)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    return;
                }

                await next();
            });

            app.MapControllers();
            app.Run();

        }
    }
}

