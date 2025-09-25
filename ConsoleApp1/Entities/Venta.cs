namespace Entities
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime? FechaVenta { get; set; }
        public int? Cantidad { get; set; }
        public decimal? MontoTotal { get; set; }

    }
}