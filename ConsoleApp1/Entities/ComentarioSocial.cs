namespace Entities
{
    public class ComentarioSocial
    {
        public int IdComentario { get; set; }
        public int? IdFuente { get; set; }
        public int? IdCliente { get; set; }
        public string TextoComentario { get; set; }
        public DateTime? FechaComentario { get; set; }
        public int Reacciones { get; set; }
    }
}