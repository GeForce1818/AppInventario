using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
    public class ProductoDetailModel
    {
        public string producto_id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal? preciounitario { get; set; }
        public int? categoria_id { get; set; }
        public string inventario_id { get; set; }
        public string inventario_nombre { get; set; }
        public string categoria_nombre { get; set; }
		public int? cantidad_total { get; set; }
        public string? estado { get; set; }
	}
}
