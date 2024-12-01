using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class ProductoNuevo
{
    public string ProductoNuevoId { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? PrecioEstimado { get; set; }

    public int? CategoriaId { get; set; }

    public int? EstadoRegistro { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<DetallePedidoNuevo> DetallePedidoNuevos { get; set; } = new List<DetallePedidoNuevo>();

    public virtual Registro? EstadoRegistroNavigation { get; set; }
}
