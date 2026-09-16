using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainngCenter.Api.Common.Mapping;
using TrainngCenter.Api.Services.Interfaces;
using TrainngCenter.Api.Services;
using AutoMapper;
using TrainingCenter.Api.Services.Interfaces;
using TrainingCenter.Api.Services;

namespace TrainingCenter.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSingleton<IMapper>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.AddProfile<MappingProfile>();
                    },
                    loggerFactory);

                return config.CreateMapper();
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IStudentService, StudentService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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