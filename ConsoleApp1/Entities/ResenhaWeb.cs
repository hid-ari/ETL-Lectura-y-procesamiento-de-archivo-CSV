namespace Entities
{
    public class ResenhaWeb
    {   
        public int IdResenha { get; set; }
        public int? IdFuente { get; set; }
        public int? IdProducto { get; set; }
        public int? Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaResenha { get; set; }
    }
}