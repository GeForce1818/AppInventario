using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class EstadoLote
{
    public int EstadoId { get; set; }

    public string? EstadoNombre { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();
}
