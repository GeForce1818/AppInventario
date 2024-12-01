using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class EstadoStock
{
    public int EstadoId { get; set; }

    public string? EstadoNombre { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
