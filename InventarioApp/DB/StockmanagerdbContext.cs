using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InventarioApp.DB;

public partial class StockmanagerdbContext : DbContext
{
    public StockmanagerdbContext()
    {
    }

    public StockmanagerdbContext(DbContextOptions<StockmanagerdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DetallePedidoNuevo> DetallePedidoNuevos { get; set; }

    public virtual DbSet<EstadoLote> EstadoLotes { get; set; }

    public virtual DbSet<EstadoStock> EstadoStocks { get; set; }

    public virtual DbSet<ExtraccionProducto> ExtraccionProductos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoNuevo> PedidoNuevos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoNuevo> ProductoNuevos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.Property(e => e.CategoriaId)
                .ValueGeneratedNever()
                .HasColumnName("categoria_id");
            entity.Property(e => e.CategoriaNombre)
                .HasMaxLength(100)
                .HasColumnName("categoria_nombre");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => new { e.PedidoId, e.ProductoId }).HasName("detalle_pedido_pkey");

            entity.ToTable("detalle_pedido");

            entity.Property(e => e.PedidoId)
                .HasMaxLength(100)
                .HasColumnName("pedido_id");
            entity.Property(e => e.ProductoId)
                .HasMaxLength(100)
                .HasColumnName("producto_id");
            entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");
            entity.Property(e => e.FechaFabricacion).HasColumnName("fecha_fabricacion");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.LoteId)
                .HasMaxLength(100)
                .HasColumnName("lote_id");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_pedido_pedido_id_fkey");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_pedido_producto_id_fkey");
        });

        modelBuilder.Entity<DetallePedidoNuevo>(entity =>
        {
            entity.HasKey(e => new { e.PedidoNuevoId, e.ProductoNuevoId }).HasName("detalle_pedido_nuevo_pkey");

            entity.ToTable("detalle_pedido_nuevo");

            entity.Property(e => e.PedidoNuevoId)
                .HasMaxLength(100)
                .HasColumnName("pedido_nuevo_id");
            entity.Property(e => e.ProductoNuevoId)
                .HasMaxLength(100)
                .HasColumnName("producto_nuevo_id");
            entity.Property(e => e.CantidadSolicitada).HasColumnName("cantidad_solicitada");
            entity.Property(e => e.FechaFabricacion).HasColumnName("fecha_fabricacion");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.LoteId)
                .HasMaxLength(100)
                .HasColumnName("lote_id");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.PedidoNuevo).WithMany(p => p.DetallePedidoNuevos)
                .HasForeignKey(d => d.PedidoNuevoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_pedido_nuevo_pedido_nuevo_id_fkey");

            entity.HasOne(d => d.ProductoNuevo).WithMany(p => p.DetallePedidoNuevos)
                .HasForeignKey(d => d.ProductoNuevoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_pedido_nuevo_producto_nuevo_id_fkey");
        });

        modelBuilder.Entity<EstadoLote>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("estado_lote_pkey");

            entity.ToTable("estado_lote");

            entity.Property(e => e.EstadoId)
                .ValueGeneratedNever()
                .HasColumnName("estado_id");
            entity.Property(e => e.EstadoNombre)
                .HasMaxLength(100)
                .HasColumnName("estado_nombre");
        });

        modelBuilder.Entity<EstadoStock>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("estado_stock_pkey");

            entity.ToTable("estado_stock");

            entity.Property(e => e.EstadoId)
                .ValueGeneratedNever()
                .HasColumnName("estado_id");
            entity.Property(e => e.EstadoNombre)
                .HasMaxLength(100)
                .HasColumnName("estado_nombre");
        });

        modelBuilder.Entity<ExtraccionProducto>(entity =>
        {
            entity.HasKey(e => e.ExtraccionId).HasName("extraccion_producto_pkey");

            entity.ToTable("extraccion_producto");

            entity.Property(e => e.ExtraccionId)
				.HasMaxLength(100)
				.HasColumnName("extraccion_id");
            entity.Property(e => e.CantidadExtraida).HasColumnName("cantidad_extraida");
            entity.Property(e => e.FechaExtraccion).HasColumnName("fecha_extraccion");
            entity.Property(e => e.LoteId)
                .HasMaxLength(50)
                .HasColumnName("lote_id");
            entity.Property(e => e.ProductoId)
                .HasMaxLength(50)
                .HasColumnName("producto_id");

            entity.HasOne(d => d.Lote).WithMany(p => p.ExtraccionProductos)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("extraccion_producto_lote_id_fkey");

            entity.HasOne(d => d.Producto).WithMany(p => p.ExtraccionProductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("extraccion_producto_producto_id_fkey");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("inventario_pkey");

            entity.ToTable("inventario");

            entity.Property(e => e.InventarioId)
                .HasMaxLength(255)
                .HasColumnName("inventario_id");
            entity.Property(e => e.InventarioNombre)
                .HasMaxLength(100)
                .HasColumnName("inventario_nombre");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.LoteId).HasName("lote_pkey");

            entity.ToTable("lote");

            entity.Property(e => e.LoteId)
                .HasMaxLength(50)
                .HasColumnName("lote_id");
            entity.Property(e => e.FechaFabricacion).HasColumnName("fecha_fabricacion");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.LoteEstado).HasColumnName("lote_estado");
            entity.Property(e => e.LoteUbicacion)
                .HasMaxLength(200)
                .HasColumnName("lote_ubicacion");
            entity.Property(e => e.ProductoId)
                .HasMaxLength(50)
                .HasColumnName("producto_id");

            entity.HasOne(d => d.LoteEstadoNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.LoteEstado)
                .HasConstraintName("lote_lote_estado_fkey");

            entity.HasOne(d => d.Producto).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lote_producto_id_fkey");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("pedido_pkey");

            entity.ToTable("pedido");

            entity.Property(e => e.PedidoId)
                .HasMaxLength(100)
                .HasColumnName("pedido_id");
            entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
            entity.Property(e => e.PedidoFecha).HasColumnName("pedido_fecha");
            entity.Property(e => e.ProveedorId)
                .HasMaxLength(100)
                .HasColumnName("proveedor_id");
            entity.Property(e => e.TipoPagoId)
                .HasMaxLength(100)
                .HasColumnName("tipo_pago_id");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ProveedorId)
                .HasConstraintName("pedido_proveedor_id_fkey");

            entity.HasOne(d => d.TipoPago).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.TipoPagoId)
                .HasConstraintName("pedido_tipo_pago_id_fkey");
        });

        modelBuilder.Entity<PedidoNuevo>(entity =>
        {
            entity.HasKey(e => e.PedidoNuevoId).HasName("pedido_nuevo_pkey");

            entity.ToTable("pedido_nuevo");

            entity.Property(e => e.PedidoNuevoId)
                .HasMaxLength(100)
                .HasColumnName("pedido_nuevo_id");
            entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
            entity.Property(e => e.PedidoNuevoFecha).HasColumnName("pedido_nuevo_fecha");
            entity.Property(e => e.ProveedorId)
                .HasMaxLength(100)
                .HasColumnName("proveedor_id");
            entity.Property(e => e.TipoPagoId)
                .HasMaxLength(100)
                .HasColumnName("tipo_pago_id");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.PedidoNuevos)
                .HasForeignKey(d => d.ProveedorId)
                .HasConstraintName("pedido_nuevo_proveedor_id_fkey");

            entity.HasOne(d => d.TipoPago).WithMany(p => p.PedidoNuevos)
                .HasForeignKey(d => d.TipoPagoId)
                .HasConstraintName("pedido_nuevo_tipo_pago_id_fkey");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("producto_pkey");

            entity.ToTable("producto");

            entity.Property(e => e.ProductoId)
                .HasMaxLength(50)
                .HasColumnName("producto_id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.InventarioId)
                .HasMaxLength(255)
                .HasColumnName("inventario_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciounitario)
                .HasPrecision(10, 2)
                .HasColumnName("preciounitario");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("producto_categoria_id_fkey");

            entity.HasOne(d => d.Inventario).WithMany(p => p.Productos)
                .HasForeignKey(d => d.InventarioId)
                .HasConstraintName("producto_inventario_id_fkey");
        });

        modelBuilder.Entity<ProductoNuevo>(entity =>
        {
            entity.HasKey(e => e.ProductoNuevoId).HasName("producto_nuevo_pkey");

            entity.ToTable("producto_nuevo");

            entity.Property(e => e.ProductoNuevoId)
                .HasMaxLength(100)
                .HasColumnName("producto_nuevo_id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.EstadoRegistro).HasColumnName("estado_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioEstimado)
                .HasPrecision(10, 2)
                .HasColumnName("precio_estimado");

            entity.HasOne(d => d.Categoria).WithMany(p => p.ProductoNuevos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("producto_nuevo_categoria_id_fkey");

            entity.HasOne(d => d.EstadoRegistroNavigation).WithMany(p => p.ProductoNuevos)
                .HasForeignKey(d => d.EstadoRegistro)
                .HasConstraintName("producto_nuevo_estado_registro_fkey");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("proveedor_pkey");

            entity.ToTable("proveedor");

            entity.Property(e => e.ProveedorId)
                .HasMaxLength(50)
                .HasColumnName("proveedor_id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Numerotelefono)
                .HasMaxLength(15)
                .HasColumnName("numerotelefono");
            entity.Property(e => e.ProveedorNombre)
                .HasMaxLength(100)
                .HasColumnName("proveedor_nombre");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.RegistroId).HasName("registro_pkey");

            entity.ToTable("registro");

            entity.Property(e => e.RegistroId)
                .ValueGeneratedNever()
                .HasColumnName("registro_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("stock_pkey");

            entity.ToTable("stock");

            entity.Property(e => e.StockId)
                .HasMaxLength(255)
                .HasColumnName("stock_id");
            entity.Property(e => e.CantidadDisponible).HasColumnName("cantidad_disponible");
            entity.Property(e => e.LoteId)
                .HasMaxLength(50)
                .HasColumnName("lote_id");
            entity.Property(e => e.StockEstado).HasColumnName("stock_estado");

            entity.HasOne(d => d.Lote).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.LoteId)
                .HasConstraintName("stock_lote_id_fkey");

            entity.HasOne(d => d.StockEstadoNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StockEstado)
                .HasConstraintName("stock_stock_estado_fkey");
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.TipoPagoId).HasName("tipo_pago_pkey");

            entity.ToTable("tipo_pago");

            entity.Property(e => e.TipoPagoId)
                .HasMaxLength(100)
                .HasColumnName("tipo_pago_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.UsuarioId)
                .ValueGeneratedNever()
                .HasColumnName("usuario_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(200)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Numerotelefono)
                .HasMaxLength(15)
                .HasColumnName("numerotelefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
