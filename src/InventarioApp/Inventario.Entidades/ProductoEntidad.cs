namespace Inventario.Entidades
{
    public class ProductoEntidad
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public class ResumenCategoriaEntidad
    {
        public string NombreCategoria { get; set; }
        public int TotalProductos { get; set; }
        public decimal ValorTotalInventario { get; set; }
        public decimal PrecioPromedio { get; set; }
        public int StockMinimo { get; set; }
    }
}
