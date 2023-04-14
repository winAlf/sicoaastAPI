using Microsoft.EntityFrameworkCore;
using sicoaastAPI.Models;

namespace sicoaastAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Organismo> Organismos { get; set; }
        public DbSet<Ccosto> Ccostos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<MovEmp> MovEmp { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
