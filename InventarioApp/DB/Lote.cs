using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Lote
{
    public string LoteId { get; set; } = null!;

    public string ProductoId { get; set; } = null!;

    public DateOnly? FechaFabricacion { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public string? LoteUbicacion { get; set; }

    public int? LoteEstado { get; set; }

    public virtual ICollection<ExtraccionProducto> ExtraccionProductos { get; set; } = new List<ExtraccionProducto>();

    public virtual EstadoLote? LoteEstadoNavigation { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
