using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
    public class LoteDetailModel
    {
        public string lote_id {  get; set; }
        public string producto_id { get; set; }
        public string nombre { get; set; }
        public DateOnly? fecha_fabricacion { get; set; }
        public DateOnly? fecha_vencimiento { get; set; }
        public string lote_ubicacion { get; set; }
        public int? lote_estado { get; set; }
        public string estado_nombre { get; set; }
    }
}
