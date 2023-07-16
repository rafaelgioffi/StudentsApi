using StudentsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentsApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1000101,
                    Name = "Rafael Gioffi",
                    Email = "rafael.gioffi@studentsapi.com",
                    Age = 38
                },
                new Student
                {
                    Id = 1000102,
                    Name = "João das Couves",
                    Email = "joao.couves@studentsapi.com",
                    Age = 17
                }
                );
        }
    }
}
