using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class PedidoNuevo
{
    public string PedidoNuevoId { get; set; } = null!;

    public DateOnly PedidoNuevoFecha { get; set; }

    public string? ProveedorId { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string? TipoPagoId { get; set; }

    public virtual ICollection<DetallePedidoNuevo> DetallePedidoNuevos { get; set; } = new List<DetallePedidoNuevo>();

    public virtual Proveedor? Proveedor { get; set; }

    public virtual TipoPago? TipoPago { get; set; }
}
