using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class DetallePedido
{
    public string PedidoId { get; set; } = null!;

    public string ProductoId { get; set; } = null!;

    public int? CantidadSolicitada { get; set; }

    public string? LoteId { get; set; }

    public DateOnly? FechaFabricacion { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
