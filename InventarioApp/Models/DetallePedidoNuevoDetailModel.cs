using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
	public class DetallePedidoNuevoDetailModel
	{
		public string pedido_nuevo_id { get; set; }
		public string producto_nuevo_id { get; set; }
		public string nombre { get; set; }
		public int? cantidad_solicitada { get; set; }
		public string? lote_id { get; set; }
		public DateOnly? fecha_fabricacion { get; set; }
		public DateOnly? fecha_vencimiento { get; set; }
		public decimal? precio_unitario { get; set; }
		public decimal Subtotal
		{
			get
			{
				return ((decimal)(cantidad_solicitada * precio_unitario));
			}
		}
	}
}
