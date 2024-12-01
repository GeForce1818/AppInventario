using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class TipoPago
{
    public string TipoPagoId { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<PedidoNuevo> PedidoNuevos { get; set; } = new List<PedidoNuevo>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
