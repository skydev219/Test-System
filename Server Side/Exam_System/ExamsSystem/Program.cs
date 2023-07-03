using ExamsSystem.Models;
using ExamsSystem.Repository;
using ExamsSystem.Repository.Grades;
using ExamsSystem.Repository.IEntities;
using ExamsSystem.Repository.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
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
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
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

            #region JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    RoleClaimType = ClaimTypes.Role,

                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("Admin"));
                options.AddPolicy("Student",
                    policy => policy.RequireRole("Student"));
                options.AddPolicy("Student,Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Student", "Admin");
                });
            });
            #endregion

            #region DI
            builder.Services.AddScoped<IEntityRepository<Exam>, ExamRepository>();
            builder.Services.AddScoped<IExam, ExamRepository>();
            builder.Services.AddScoped<IEntityRepository<Admin>, AdminRepository>();
            builder.Services.AddScoped<IEntityRepository<Student>, StudentRepository>();
            builder.Services.AddScoped<IEntityRepository<Question>, QuestionRepository>();
            builder.Services.AddScoped<IEntityRepository<Answer>, AnswerRepository>();
            builder.Services.AddScoped<IGrades, GradesRepository>();
            builder.Services.AddScoped<IJWT, JWTRepository>();
            builder.Services.AddScoped<IAuthentication<Admin>, AdminRepository>();
            builder.Services.AddScoped<IAuthentication<Student>, StudentRepository>();
            builder.Services.AddScoped<IStudentAuth<Student>, StudentRepository>();

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

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examination System V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("AllowAll");

            app.Run();
        }
    }
}