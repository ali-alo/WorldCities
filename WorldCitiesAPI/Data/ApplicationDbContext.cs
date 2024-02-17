using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WorldCitiesAPI.Data.Models;

namespace WorldCitiesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> Cities => Set<City>();
        public DbSet<Country> Countries => Set<Country>();

        public ApplicationDbContext() : base()
        {
            
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        
        }
    }
}
