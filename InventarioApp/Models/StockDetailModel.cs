using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.Models
{
    public class StockDetailModel
    {
        public string stock_id {  get; set; }
        public string lote_id { get; set; }
        public int? cantidad_disponible { get; set; }
        public int? stock_estado { get; set; }
        public string estado_nombre { get; set; }
    }
}
