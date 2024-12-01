using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Categorium
{
    public int CategoriaId { get; set; }

    public string? CategoriaNombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<ProductoNuevo> ProductoNuevos { get; set; } = new List<ProductoNuevo>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
