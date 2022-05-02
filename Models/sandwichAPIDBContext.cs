using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using sandwichAPI.Models;

namespace sandwichAPI.Models
{
    public class sandwichAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public sandwichAPIDBContext( DbContextOptions<sandwichAPIDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring( DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("sandwichAPI");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<sandwich> sandwich { get; set; } = null!;
        public DbSet<sandwichIngredients> sandwichIngredients { get; set; } = null!;

    }
}
