namespace Entities
{
    public class Opinion
    {
        public int IdOpinion { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaOpinion { get; set; }
    }
}
