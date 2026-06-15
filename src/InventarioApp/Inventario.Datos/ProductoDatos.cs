using System.Collections.Generic;
using System.Linq;
using Inventario.Entidades;

namespace Inventario.Datos
{
    public static class ProductoDatos
    {
        // READ: Obtener todos los productos ordenados por nombre
        public static List<ProductoEntidad> ObtenerTodos()
        {
            using (var contexto = new InventarioContext())
            {
                return contexto.Productos
                    .OrderBy(p => p.Nombre)
                    .Select(p => new ProductoEntidad
                    {
                        ID = p.ID,
                        Nombre = p.Nombre,
                        Categoria = p.Categoria.Nombre,
                        Precio = p.Precio,
                        Stock = p.Stock
                    })
                    .ToList();
            }
        }

        // READ: Obtener productos con stock bajo (critico)
        public static List<ProductoEntidad> ObtenerStockBajo(int umbral)
        {
            using (var contexto = new InventarioContext())
            {
                return contexto.Productos
                    .Where(p => p.Stock < umbral)
                    .OrderBy(p => p.Stock)
                    .Select(p => new ProductoEntidad
                    {
                        ID = p.ID,
                        Nombre = p.Nombre,
                        Categoria = p.Categoria.Nombre,
                        Precio = p.Precio,
                        Stock = p.Stock
                    })
                    .ToList();
            }
        }

        // AGGREGATE: Reporte de inventario agrupado por categoria
        public static List<ResumenCategoriaEntidad> ObtenerResumenPorCategoria()
        {
            using (var contexto = new InventarioContext())
            {
                return contexto.Productos
                    .GroupBy(p => p.Categoria.Nombre)
                    .Select(grupo => new ResumenCategoriaEntidad
                    {
                        NombreCategoria   = grupo.Key,
                        TotalProductos    = grupo.Count(),
                        ValorTotalInventario = grupo.Sum(p => p.Precio * p.Stock),
                        PrecioPromedio    = grupo.Average(p => p.Precio),
                        StockMinimo       = grupo.Min(p => p.Stock)
                    })
                    .OrderByDescending(r => r.ValorTotalInventario)
                    .ToList();
            }
        }

        // CREATE
        public static void InsertarProducto(ProductoEntidad nuevo)
        {
            using (var contexto = new InventarioContext())
            {
                contexto.Productos.Add(new Producto
                {
                    Nombre = nuevo.Nombre,
                    Precio = nuevo.Precio,
                    Stock  = nuevo.Stock
                });
                contexto.SaveChanges();
            }
        }

        // UPDATE: Actualizar stock tras una venta
        public static bool ActualizarStock(int idProducto, int nuevaCantidad)
        {
            using (var contexto = new InventarioContext())
            {
                var producto = contexto.Productos
                    .FirstOrDefault(p => p.ID == idProducto);

                if (producto == null) return false;

                producto.Stock = nuevaCantidad;
                contexto.SaveChanges();
                return true;
            }
        }
    }
}
