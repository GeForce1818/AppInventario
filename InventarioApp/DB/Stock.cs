using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Stock
{
    public string StockId { get; set; } = null!;

    public string? LoteId { get; set; }

    public int? CantidadDisponible { get; set; }

    public int? StockEstado { get; set; }

    public virtual Lote? Lote { get; set; }

    public virtual EstadoStock? StockEstadoNavigation { get; set; }
}
