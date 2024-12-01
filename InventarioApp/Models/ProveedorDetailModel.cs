using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
	public class ProveedorDetailModel
	{
		public string proveedor_id { get; set; }
		public string? proveedor_nombre { get; set; }
		public string? numerotelefono { get; set; }
		public string? email { get; set; }
		public string? direccion {  get; set; }
	}
}
