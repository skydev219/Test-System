using ExamsSystem.Models;
using ExamsSystem.Repository;
using ExamsSystem.Repository.IEntities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ExamsSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);


            #region Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            #endregion

            #region Json Serializer
            builder.Services.AddMvc()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            #endregion


            #region Connection
            // Add services to the container.

            builder.Services.AddDbContext<SchoolContext>(db =>
            db.UseSqlServer(
                builder.Configuration.GetConnectionString("conn")
                )
            );
            #endregion

            #region DI
            builder.Services.AddScoped<IEntityRepository<Exam>, ExamRepository>();
            builder.Services.AddScoped<IExam, ExamRepository>();
            builder.Services.AddScoped<IEntityRepository<Admin>, AdminRepository>();

            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication? app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("AllowAll"); // Alow Cors

            app.Run();
        }
    }
}