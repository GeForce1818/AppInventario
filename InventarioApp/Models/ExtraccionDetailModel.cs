using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
    public class ExtraccionDetailModel
    {
        public string extraccion_id { get; set; }
        public string producto_id { get; set; }
        public string lote_id { get; set; }
        public int cantidad_extraida {  get; set; }
        public DateOnly fecha_extraccion { get; set; }
        public string nombre { get; set; }
	}
}
