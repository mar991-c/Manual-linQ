using System.Collections.Generic;
using System.Linq;
using Inventario.Entidades;

namespace Inventario.Negocio
{
    public static class ProductoNegocio
    {
        // LINQ to Objects: Filtrar por categoria en memoria
        public static List<ProductoEntidad> ObtenerPorCategoria(string categoria)
        {
            List<ProductoEntidad> todos = ProductoDatos.ObtenerTodos();

            return todos
                .Where(p => p.Categoria.ToLower() == categoria.ToLower())
                .ToList();
        }

        // Regla de negocio: no se puede vender si no hay stock suficiente
        public static string ProcesarVenta(int idProducto, int cantidadVendida)
        {
            var producto = ProductoDatos.ObtenerTodos()
                .FirstOrDefault(p => p.ID == idProducto);

            if (producto == null)
                return "Error: Producto no encontrado.";

            if (producto.Stock < cantidadVendida)
                return $"Error: Stock insuficiente. Disponible: {producto.Stock} unidades.";

            int nuevoStock = producto.Stock - cantidadVendida;
            ProductoDatos.ActualizarStock(idProducto, nuevoStock);
            return $"Venta registrada. Stock actualizado a {nuevoStock} unidades.";
        }

        // Exponer el reporte de resumen a la Capa de Presentacion
        public static List<ResumenCategoriaEntidad> ObtenerResumenInventario()
        {
            return ProductoDatos.ObtenerResumenPorCategoria();
        }
    }
}
