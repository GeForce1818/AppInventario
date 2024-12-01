using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class DetallePedidoNuevo
{
    public string PedidoNuevoId { get; set; } = null!;

    public string ProductoNuevoId { get; set; } = null!;

    public string? LoteId { get; set; }

    public int? CantidadSolicitada { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public DateOnly? FechaFabricacion { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public virtual PedidoNuevo PedidoNuevo { get; set; } = null!;

    public virtual ProductoNuevo ProductoNuevo { get; set; } = null!;
}
