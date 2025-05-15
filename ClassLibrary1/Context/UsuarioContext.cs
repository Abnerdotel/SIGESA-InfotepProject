

using Microsoft.EntityFrameworkCore;
using SigesaEntidades;

namespace SigesaData.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {


                optionsBuilder.UseInMemoryDatabase("SigedaDb");
                
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<RolUsuario> Categories { get; set; }
        public DbSet<Usuario> Products { get; set; }
    }
}
