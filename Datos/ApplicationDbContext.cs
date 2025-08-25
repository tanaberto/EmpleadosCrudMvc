using Microsoft.EntityFrameworkCore;

namespace EmpleadosCrudMvc.Datos
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Models.Empleado> Empleados { get; set; }
	}
}
