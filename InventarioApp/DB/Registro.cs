using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Registro
{
    public int RegistroId { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<ProductoNuevo> ProductoNuevos { get; set; } = new List<ProductoNuevo>();
}
