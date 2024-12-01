using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
	public class UsuarioDetailModel
	{
		public string usuario_id {  get; set; }
		public string nombre { get; set; }
		public string apellido { get; set; }
		public string numerotelefono { get; set; }
		public string email { get; set; }
		public string direccion { get; set; }
		public string contrasena { get; set; }
	}
}
