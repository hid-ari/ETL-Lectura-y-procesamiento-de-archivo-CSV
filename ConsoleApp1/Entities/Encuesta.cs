namespace Entities
{
    public class Encuesta
    {
        public int IdEncuesta { get; set; }
        public int? IdCliente { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public DateTime? FechaEncuesta { get; set; }
    }
}