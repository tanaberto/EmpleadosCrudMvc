using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpleadosCrudMvc.Datos;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosCrudMvc.Pages.Api
{
    public class EmpleadosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosModel(ApplicationDbContext context)
        {
            _context = context;
        }

		/*
        public async Task<IActionResult> OnGetAsync()
        {
            var empleados = await _context.Empleados.ToListAsync();
            return new JsonResult(empleados);
        }
		*/

		public async Task<IActionResult> OnGetAsyncEmpleados()
		{
			// Llama al procedimiento almacenado que retorna empleados
			var empleados = await _context.Empleados
				.FromSqlRaw("EXEC ObtenerEmpleados")
				.ToListAsync();

			return new JsonResult(empleados);
		}
	}
}