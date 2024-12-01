using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Producto
{
    public string ProductoId { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Preciounitario { get; set; }

    public int? CategoriaId { get; set; }

    public string? InventarioId { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<ExtraccionProducto> ExtraccionProductos { get; set; } = new List<ExtraccionProducto>();

    public virtual Inventario? Inventario { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();
}
