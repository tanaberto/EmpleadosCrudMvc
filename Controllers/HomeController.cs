using EmpleadosCrudMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace EmpleadosCrudMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;

		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		//public IActionResult Index()
		public async Task<IActionResult> Index()
		{
			var httpClient = _httpClientFactory.CreateClient();
			var empleados = await httpClient.GetFromJsonAsync<List<Empleado>>("http://localhost:5052/api/Empleados");

			return View(empleados);
		}



        public IActionResult Privacy()
        {
            return View();
        }

		
		public IActionResult Agregar()
		{

			return View();
		}
		

		/// <summary>
		/// Agergar un nuevo empleado
		/// </summary>
		/// <param name="empleado"></param>
		/// <param name="httpClientFactory"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Agregar(Empleado empleado, [FromServices] IHttpClientFactory httpClientFactory)
		{
			var httpClient = httpClientFactory.CreateClient();
			var response = await httpClient.PostAsJsonAsync("http://localhost:5052/api/Empleados", empleado);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			TempData["ErrorMessage"] = "Error al agregar el empleado.";

			ModelState.AddModelError(string.Empty, "Error al agregar el empleado.");
			return View(empleado);
		}

		/// <summary>
		/// Eliminar un empleado
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Eliminar()
		{
			var httpClient = _httpClientFactory.CreateClient();
			var empleados = await httpClient.GetFromJsonAsync<List<Empleado>>("http://localhost:5052/api/Empleados");

			return View(empleados);
		}

		[HttpPost]
		public async Task<IActionResult> Eliminar(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			var response = await httpClient.DeleteAsync($"http://localhost:5052/api/Empleados/{id}");

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			TempData["ErrorMessage"] = "Error al eliminar el empleado.";
			//lista de empleados o mostrar un mensaje de error
			return RedirectToAction("Eliminar");
		}


		/// <summary>
		/// Actualizar un empleado
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Actualizar()
		{
			var httpClient = _httpClientFactory.CreateClient();
			var empleados = await httpClient.GetFromJsonAsync<List<Empleado>>("http://localhost:5052/api/Empleados");

			return View(empleados);
		}


		[HttpPost]
		public async Task<IActionResult> ActualizarEmpleado(int Codigo, string Nombre, string Puesto, int CodigoJefe)
		{
			var httpClient = _httpClientFactory.CreateClient();
			var empleado = new Empleado { Codigo = Codigo, Nombre = Nombre, Puesto = Puesto, CodigoJefe = CodigoJefe };
			var response = await httpClient.PutAsJsonAsync($"http://localhost:5052/api/Empleados/{Codigo}", empleado);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Actualizar");
			}
				

			TempData["ErrorMessage"] = "Error al actualizar el empleado.";
			return RedirectToAction("Actualizar");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
