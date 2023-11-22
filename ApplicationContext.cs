using Microsoft.EntityFrameworkCore;

namespace AngularAPI2
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<UserModel>().HasData(
                new UserModel() { Id=1, Name="Хуй", Age=23 },
                 new UserModel() { Id = 2, Name = "Хуй с маслом", Age = 265 },
                 new UserModel() { Id = 3, Name = "Хуй с икрой", Age = 233 }
                );*/
        }
    }
}
