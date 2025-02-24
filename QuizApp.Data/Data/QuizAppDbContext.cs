using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.WebAPI.Data
{
    public class QuizAppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options) { }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questiones { get; set; }
        public DbSet<Answer> Answeres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Role (N-N)
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            // Quiz - Question (N-N)
            modelBuilder.Entity<QuizQuestion>().HasKey(qq => new { qq.QuizId, qq.QuestionId });

            modelBuilder.Entity<QuizQuestion>()
                .HasOne(qq => qq.Quiz)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuizQuestion>()
                .HasOne(qq => qq.Question)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(qq => qq.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserQuiz - UserAnswer (1-N)
            modelBuilder.Entity<UserQuiz>()
                .HasMany(uq => uq.UserAnswers)
                .WithOne(ua => ua.UserQuiz)
                .HasForeignKey(ua => ua.UserQuizId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserQuiz>()
                .HasMany(uq => uq.UserAnswers)
                .WithOne(ua => ua.UserQuiz)
                .HasForeignKey(ua => ua.UserQuizId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
