using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
	public class ProductoNuevoDetailModel
	{
		public string producto_nuevo_id { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public decimal? precio_estimado { get; set; }
		public int? categoria_id { get; set; }
		public string categoria_nombre { get; set; }
		public int? estado_registro { get; set; }
		public string? estado { get; set; }
	}
}
