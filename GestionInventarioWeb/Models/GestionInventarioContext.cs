using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GestionInventarioWeb.Models;

public partial class GestionInventarioContext : DbContext
{
    public GestionInventarioContext()
    {
    }

    public GestionInventarioContext(DbContextOptions<GestionInventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<ItemCompra> ItemCompras { get; set; }

    public virtual DbSet<ItemVentum> ItemVenta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasIndex(e => e.Categoria1, "Unique_categoria").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria1)
                .HasMaxLength(50)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Usuarios");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.ToTable("Inventario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Productos");
        });

        modelBuilder.Entity<ItemCompra>(entity =>
        {
            entity.ToTable("Item_Compra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("((1))")
                .HasColumnName("cantidad");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.ItemCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Compra_Compras");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ItemCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Compra_Productos");
        });

        modelBuilder.Entity<ItemVentum>(entity =>
        {
            entity.ToTable("Item_Venta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("((1))")
                .HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ItemVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Venta_Productos");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.ItemVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Venta_Ventas");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Ean)
                .HasMaxLength(50)
                .HasColumnName("ean");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Rol, "Unique_rol").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.Rut, "Unique_rut").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .HasColumnName("clave");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Rut)
                .HasMaxLength(12)
                .HasColumnName("rut");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Usuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
