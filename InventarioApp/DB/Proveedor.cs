using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Proveedor
{
    public string ProveedorId { get; set; } = null!;

    public string? ProveedorNombre { get; set; }

    public string? Numerotelefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<PedidoNuevo> PedidoNuevos { get; set; } = new List<PedidoNuevo>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
