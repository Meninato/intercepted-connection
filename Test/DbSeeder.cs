using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class DbSeeder
    {
        private readonly ModelBuilder _modelBuilder;

        public DbSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            SeedUsers();
        }

        public void SeedUsers()
        {
            _modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Lula"
                },
                new User
                {
                    Id = 2,
                    Name = "Obama"
                },
                new User
                { 
                    Id = 3,
                    Name = "Silvia"
                }
            );
        }
    }
}