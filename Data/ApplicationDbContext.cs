using IdentityCourse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityCourse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUser>().ToTable("Users");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<Student>().ToTable("StudentsModel");
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Marketier" });
        }
        public DbSet<Student> Students { get; set; } 
    }
}
