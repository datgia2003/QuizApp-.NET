using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizApp.WebAPI.Models;

namespace QuizApp.WebAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(QuizAppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Đảm bảo database đã được tạo
            context.Database.Migrate();

            //Seed dữ liệu Role trước
            var roles = new List<string> { "Admin", "Instructor", "Student" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Role { Name = roleName, Description = $"{roleName} Role" });
                }
            }

            // Seed dữ liệu User
            if (!userManager.Users.Any())
            {
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    EmailConfirmed = true
                };

                var instructor = new User
                {
                    UserName = "instructor",
                    Email = "instructor@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    EmailConfirmed = true
                };

                var student = new User
                {
                    UserName = "student",
                    Email = "student@example.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(2000, 10, 20),
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.CreateAsync(instructor, "Instructor@123");
                await userManager.CreateAsync(student, "Student@123");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(instructor, "Instructor");
                await userManager.AddToRoleAsync(student, "Student");
            }

            //Seed dữ liệu Quiz
            if (!context.Quizzes.Any())
            {
                var quizzes = new List<Quiz>
                {
                    new Quiz { Id = Guid.NewGuid(), Title = "C# Basics", Description = "Basic C# Questions", Duration = 600 },
                    new Quiz { Id = Guid.NewGuid(), Title = "ASP.NET Core", Description = "ASP.NET Core Questions", Duration = 900 },
                    new Quiz { Id = Guid.NewGuid(), Title = "Entity Framework", Description = "EF Core Questions", Duration = 800 },
                    new Quiz { Id = Guid.NewGuid(), Title = "SQL Queries", Description = "SQL Related Questions", Duration = 700 },
                    new Quiz { Id = Guid.NewGuid(), Title = "OOP Principles", Description = "Object Oriented Programming", Duration = 750 }
                };

                context.Quizzes.AddRange(quizzes);
                await context.SaveChangesAsync();

                // Seed dữ liệu Question
                var questions = new List<Question>
                {
                    new Question { Id = Guid.NewGuid(), Content = "What is the default access modifier in C#?", QuestionType = "SingleChoice", QuizId = quizzes[0].Id },
                    new Question { Id = Guid.NewGuid(), Content = "Which of these is NOT a valid HTTP method?", QuestionType = "MultipleChoice", QuizId = quizzes[1].Id },
                    new Question { Id = Guid.NewGuid(), Content = "What is the purpose of DbContext in EF Core?", QuestionType = "ShortAnswer", QuizId = quizzes[2].Id },
                    new Question { Id = Guid.NewGuid(), Content = "What does SQL stand for?", QuestionType = "SingleChoice", QuizId = quizzes[3].Id },
                    new Question { Id = Guid.NewGuid(), Content = "Which of the following are OOP principles?", QuestionType = "MultipleChoice", QuizId = quizzes[4].Id }
                };

                context.Questiones.AddRange(questions);
                await context.SaveChangesAsync();

                // Seed dữ liệu Answer
                var answers = new List<Answer>
                {
                    new Answer { Id = Guid.NewGuid(), Content = "Private", IsCorrect = true, QuestionId = questions[0].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "Public", IsCorrect = false, QuestionId = questions[0].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "HEAD", IsCorrect = false, QuestionId = questions[1].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "FETCH", IsCorrect = true, QuestionId = questions[1].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "DataContext", IsCorrect = false, QuestionId = questions[2].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "Structured Query Language", IsCorrect = true, QuestionId = questions[3].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "Inheritance", IsCorrect = true, QuestionId = questions[4].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "Polymorphism", IsCorrect = true, QuestionId = questions[4].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "Encapsulation", IsCorrect = true, QuestionId = questions[4].Id },
                    new Answer { Id = Guid.NewGuid(), Content = "Compilation", IsCorrect = false, QuestionId = questions[4].Id }
                };

                context.Answeres.AddRange(answers);
                await context.SaveChangesAsync();

                // Seed dữ liệu UserQuiz (User tham gia quiz)
                var student = await userManager.FindByNameAsync("student");
                if (student != null)
                {
                    var userQuizzes = quizzes.Select(q => new UserQuiz
                    {
                        Id = Guid.NewGuid(),
                        UserId = student.Id,
                        QuizId = q.Id,
                        QuizCode = $"QUIZ-{q.Id.ToString().Substring(0, 5)}",
                        StartedAt = DateTime.UtcNow,
                        FinishedAt = DateTime.UtcNow.AddMinutes(q.Duration / 60)
                    }).ToList();

                    context.UserQuizzes.AddRange(userQuizzes);
                    await context.SaveChangesAsync();
                }
            }
        }

        //seed data qua sql script (đã seed data)
        public static async Task Initialize(QuizAppDbContext context)
        {
            if (!await context.UserQuizzes.AnyAsync())
            {
                var sql = await File.ReadAllTextAsync("userquiz_data.sql");
                await context.Database.ExecuteSqlRawAsync(sql);
            }
        }
    }
}
