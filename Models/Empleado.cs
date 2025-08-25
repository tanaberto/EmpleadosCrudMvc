using System.ComponentModel.DataAnnotations;

namespace EmpleadosCrudMvc.Models
{
	public class Empleado
	{
		[Key]
		public int Codigo { get; set; }

		[Required(ErrorMessage ="El nombre es requerido")]
		public string Nombre { get; set; }

		[Required]
		public string Puesto { get; set; }

		public int? CodigoJefe { get; set; }

	}
}
