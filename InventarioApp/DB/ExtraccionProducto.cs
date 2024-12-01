using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class ExtraccionProducto
{
    public string ExtraccionId { get; set; }

    public string ProductoId { get; set; } = null!;

    public string LoteId { get; set; } = null!;

    public int CantidadExtraida { get; set; }

    public DateOnly FechaExtraccion { get; set; }

    public virtual Lote Lote { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
