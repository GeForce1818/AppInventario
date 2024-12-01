using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
	public class PedidoDetailModel
	{
		public string pedido_id { get; set; }
		public DateOnly? pedido_fecha { get; set; }
		public string proveedor_id { get; set; }
		public string proveedor_nombre { get; set; }
		public DateOnly? fecha_entrega { get; set; }
		public string tipo_pago_id { get; set; }
		public string descripcion { get; set; }
	}
}
