using Data;
using Business;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DbContext();
            var etl = new EtlService(
                new ClienteRepository(db),
                new ProductoRepository(db),
                new FuenteDatoRepository(db),
                new EncuestaRepository(db),
                new ComentarioSocialRepository(db),
                new ResenhaWebRepository(db),
                new VentaRepository(db),          
                new OpinionRepository(db)          
            );

            etl.ProcesarClientes("clientes.csv");
            etl.ProcesarProductos("productos.csv");
            etl.ProcesarFuentes("fuentes_datos.csv");
            etl.ProcesarEncuestas("encuestas.csv");
            etl.ProcesarComentarios("comentarios_sociales.csv");
            etl.ProcesarResenhas("resenhas_web.csv");
            etl.ProcesarVentas("ventas.csv");
            etl.ProcesarOpiniones("opiniones.csv");

            Console.WriteLine("ETL finalizado y datos cargados en la base de datos MercaJumbo.");
        }
    }
}