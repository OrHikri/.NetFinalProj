 
using DataBase.Repositories;

namespace Server_Api
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

            // Dependency Injection : evrey time there is  interface: IUserDbRepository > create object of UserDbRepository
            builder.Services.AddSingleton<IUserDbRepository, UserDbRepository>();
            builder.Services.AddSingleton<ITestsDBRepository, TestsDbRepository>();
            builder.Services.AddSingleton<IQuestinDBRepository, QuestionDbRepository>();
            builder.Services.AddSingleton<IAnswerDbRepository, AnswerDbRepository>();
            builder.Services.AddSingleton<IGradeDBRepository, GradeDbRepository>();
            builder.Services.AddSingleton<IErrorDbRepository, ErrorDbRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            

            app.UseStaticFiles();

            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}