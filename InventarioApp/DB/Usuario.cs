using System;
using System.Collections.Generic;

namespace InventarioApp.DB;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Numerotelefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public string? Contrasena { get; set; }
}
