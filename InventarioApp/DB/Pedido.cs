using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Pedido
{
    public string PedidoId { get; set; } = null!;

    public DateOnly? PedidoFecha { get; set; }

    public string? ProveedorId { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string? TipoPagoId { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Proveedor? Proveedor { get; set; }

    public virtual TipoPago? TipoPago { get; set; }
}
