namespace Entities
{
    public class Producto
    {
        public int IdProducto { get; set; } 
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal? Precio { get; set; }
    }
}