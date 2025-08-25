using EmpleadosCrudMvc.Datos;
using EmpleadosCrudMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosCrudMvc.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmpleadosController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public EmpleadosController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/EmpleadosApi
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
		{
			//return await _context.Empleados.ToListAsync();

			
			// Llama al procedimiento almacenado que retorna empleados
			var empleados = await _context.Empleados
				.FromSqlRaw("EXEC ObtenerEmpleados")
				.ToListAsync();
			
			return new JsonResult(empleados);
		}

		// GET: api/EmpleadosApi/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Empleado>> GetEmpleado(int id)
		{
			var empleado = await _context.Empleados.FindAsync(id);

			if (empleado == null)
				return NotFound();

			return empleado;
		}

		// POST: api/EmpleadosApi
		[HttpPost]
		public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
		{
			_context.Empleados.Add(empleado);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Codigo }, empleado);
		}

		// PUT: api/EmpleadosApi/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
		{
			if (id != empleado.Codigo)
				return BadRequest();

			_context.Entry(empleado).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Empleados.Any(e => e.Codigo == id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/EmpleadosApi/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEmpleado(int id)
		{
			var empleado = await _context.Empleados.FindAsync(id);
			if (empleado == null)
				return NotFound();

			_context.Empleados.Remove(empleado);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
