using CEBackend;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace CEBackend.Database

{
    public class Context : DbContext
    {
        public DbSet<Alien> Aliens { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
