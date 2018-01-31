namespace LearningSystem.Data
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Course> Courses { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<StudentCourse> StudentsInCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(u => u.Courses)
                .HasForeignKey(sc => sc.StudentId);

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);

            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });


            builder
                .Entity<Course>()
                .HasOne(c => c.Trainer)
                .WithMany(u => u.Trainings)
                .HasForeignKey(c => c.TrainerId);


            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AuthorId);


            base.OnModelCreating(builder);
        }
    }
}
