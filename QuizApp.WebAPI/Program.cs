using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;
using QuizApp.Data.Repositories;
using QuizApp.Business.Interfaces;
using QuizApp.Business.Services;

namespace QuizApp.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<QuizAppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            // Đăng ký Identity
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<QuizAppDbContext>()
                .AddDefaultTokenProviders();

            // Đăng ký RoleManager và UserManager
            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<RoleManager<Role>>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IQuizRepository, QuizRepository>();

            //My Service
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped<IQuizService,QuizService>();
            builder.Services.AddScoped<IQuestionService,QuestionService>();
            builder.Services.AddScoped<IAnswerService,AnswerService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();


            var app = builder.Build();

            //seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<QuizAppDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                await SeedData.Initialize(context, userManager, roleManager);
                await SeedData.Initialize(context);
            }

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
