using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Inventario
{
    public string InventarioId { get; set; } = null!;

    public string? InventarioNombre { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
