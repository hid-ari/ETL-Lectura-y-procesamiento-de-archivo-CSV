using CsvHelper;
using CsvHelper.Configuration;
using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Business
{
    public class EtlService
    {
        private readonly ClienteRepository _clienteRepo;
        private readonly ProductoRepository _productoRepo;
        private readonly FuenteDatoRepository _fuenteRepo;
        private readonly EncuestaRepository _encuestaRepo;
        private readonly ComentarioSocialRepository _comentarioRepo;
        private readonly ResenhaWebRepository _resenhaRepo;
        private readonly VentaRepository _ventaRepo;          
        private readonly OpinionRepository _opinionRepo;    

        public EtlService(
            ClienteRepository clienteRepo,
            ProductoRepository productoRepo,
            FuenteDatoRepository fuenteRepo,
            EncuestaRepository encuestaRepo,
            ComentarioSocialRepository comentarioRepo,
            ResenhaWebRepository resenhaRepo,
            VentaRepository ventaRepo,          
            OpinionRepository opinionRepo         
        )
        {
            _clienteRepo = clienteRepo;
            _productoRepo = productoRepo;
            _fuenteRepo = fuenteRepo;
            _encuestaRepo = encuestaRepo;
            _comentarioRepo = comentarioRepo;
            _resenhaRepo = resenhaRepo;
            _ventaRepo = ventaRepo;
            _opinionRepo = opinionRepo;         
        }


        public void ProcesarClientes(string path)
        {
            var clientes = LeerCsv<Cliente>(path);
            var clientesLimpios = LimpiarClientes(clientes);
            _clienteRepo.Insertar(clientesLimpios);
        }

        public void ProcesarProductos(string path)
        {
            var productos = LeerCsv<Producto>(path);
            var productosLimpios = LimpiarProductos(productos);
            _productoRepo.Insertar(productosLimpios);
        }

        public void ProcesarFuentes(string path)
        {
            var fuentes = LeerCsv<FuenteDato>(path);
            var fuentesLimpias = LimpiarFuentes(fuentes);
            _fuenteRepo.Insertar(fuentesLimpias);
        }

        public void ProcesarEncuestas(string path)
        {
            var encuestas = LeerCsv<Encuesta>(path);
            var encuestasLimpias = LimpiarEncuestas(encuestas);
            _encuestaRepo.Insertar(encuestasLimpias);
        }

        public void ProcesarComentarios(string path)
        {
            var comentarios = LeerCsv<ComentarioSocial>(path);
            var comentariosLimpios = LimpiarComentarios(comentarios);
            _comentarioRepo.Insertar(comentariosLimpios);
        }

        public void ProcesarResenhas(string path)
        {
            var resenhas = LeerCsv<ResenhaWeb>(path);
            var resenhasLimpias = LimpiarResenhas(resenhas);
            _resenhaRepo.Insertar(resenhasLimpias);
        }

        public void ProcesarVentas(string path)   
        {
            var ventas = LeerCsv<Venta>(path);
            var ventasLimpias = LimpiarVentas(ventas);
            _ventaRepo.Insertar(ventasLimpias);
        }

        public void ProcesarOpiniones(string path)  
        {
            var opiniones = LeerCsv<Opinion>(path);
            var opinionesLimpias = LimpiarOpiniones(opiniones);
            _opinionRepo.Insertar(opinionesLimpias);
        }


        private List<Cliente> LimpiarClientes(List<Cliente> clientes)
        {
            var resultado = new List<Cliente>();
            var correos = new HashSet<string>();

            foreach (var c in clientes)
            {
                if (string.IsNullOrWhiteSpace(c.Nombre) ||
                    string.IsNullOrWhiteSpace(c.Apellido) ||
                    string.IsNullOrWhiteSpace(c.Correo) ||
                    string.IsNullOrWhiteSpace(c.Telefono) ||
                    c.FechaRegistro == default)
                    continue;

                var correo = c.Correo.Trim().ToLower();
                if (!correos.Add(correo))
                    continue;

                resultado.Add(new Cliente
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre.Trim(),
                    Apellido = c.Apellido.Trim(),
                    Correo = correo,
                    Telefono = c.Telefono.Trim(),
                    FechaRegistro = c.FechaRegistro
                });
            }
            return resultado;
        }

        private List<Producto> LimpiarProductos(List<Producto> productos)
        {
            var resultado = new List<Producto>();
            var nombres = new HashSet<string>();

            foreach (var p in productos)
            {
                if (string.IsNullOrWhiteSpace(p.NombreProducto) ||
                    string.IsNullOrWhiteSpace(p.Descripcion) ||
                    string.IsNullOrWhiteSpace(p.Categoria) ||
                    p.Precio <= 0)
                    continue;

                var nombre = p.NombreProducto.Trim().ToLower();
                if (!nombres.Add(nombre))
                    continue;

                resultado.Add(new Producto
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto.Trim(),
                    Descripcion = p.Descripcion.Trim(),
                    Categoria = p.Categoria.Trim(),
                    Precio = p.Precio
                });
            }
            return resultado;
        }

        private List<FuenteDato> LimpiarFuentes(List<FuenteDato> fuentes)
        {
            var resultado = new List<FuenteDato>();
            var nombres = new HashSet<string>();

            foreach (var f in fuentes)
            {
                if (string.IsNullOrWhiteSpace(f.NombreFuente) ||
                    string.IsNullOrWhiteSpace(f.TipoFuente) ||
                    string.IsNullOrWhiteSpace(f.URL))
                    continue;

                var nombre = f.NombreFuente.Trim().ToLower();
                if (!nombres.Add(nombre))
                    continue;

                resultado.Add(new FuenteDato
                {
                    IdFuente = f.IdFuente,
                    NombreFuente = f.NombreFuente.Trim(),
                    TipoFuente = f.TipoFuente.Trim(),
                    URL = f.URL.Trim()
                });
            }
            return resultado;
        }

        private List<Encuesta> LimpiarEncuestas(List<Encuesta> encuestas)
        {
            var resultado = new List<Encuesta>();
            var claves = new HashSet<string>();

            foreach (var e in encuestas)
            {
                if (e.IdCliente <= 0 ||
                    string.IsNullOrWhiteSpace(e.Pregunta) ||
                    string.IsNullOrWhiteSpace(e.Respuesta) ||
                    e.FechaEncuesta == default)
                    continue;

                var clave = $"{e.IdCliente}-{e.Pregunta.Trim().ToLower()}";
                if (!claves.Add(clave))
                    continue;

                resultado.Add(new Encuesta
                {
                    IdEncuesta = e.IdEncuesta,
                    IdCliente = e.IdCliente,
                    Pregunta = e.Pregunta.Trim(),
                    Respuesta = e.Respuesta.Trim(),
                    FechaEncuesta = e.FechaEncuesta
                });
            }
            return resultado;
        }

        private List<ComentarioSocial> LimpiarComentarios(List<ComentarioSocial> comentarios)
        {
            var resultado = new List<ComentarioSocial>();
            var claves = new HashSet<string>();

            foreach (var c in comentarios)
            {
                if (c.IdFuente <= 0 ||
                    c.IdCliente <= 0 ||
                    string.IsNullOrWhiteSpace(c.TextoComentario) ||
                    c.FechaComentario == default)
                    continue;

                var clave = $"{c.IdFuente}-{c.IdCliente}-{c.TextoComentario.Trim().ToLower()}";
                if (!claves.Add(clave))
                    continue;

                resultado.Add(new ComentarioSocial
                {
                    IdComentario = c.IdComentario,
                    IdFuente = c.IdFuente,
                    IdCliente = c.IdCliente,
                    TextoComentario = c.TextoComentario.Trim(),
                    FechaComentario = c.FechaComentario,
                    Reacciones = c.Reacciones
                });
            }
            return resultado;
        }

        private List<ResenhaWeb> LimpiarResenhas(List<ResenhaWeb> resenhas)
        {
            var resultado = new List<ResenhaWeb>();
            var claves = new HashSet<string>();

            foreach (var r in resenhas)
            {
                if (r.IdFuente <= 0 ||
                    r.IdProducto <= 0 ||
                    r.Calificacion < 1 || r.Calificacion > 5 ||
                    string.IsNullOrWhiteSpace(r.Comentario) ||
                    r.FechaResenha == default)
                    continue;

                var clave = $"{r.IdFuente}-{r.IdProducto}-{r.Comentario.Trim().ToLower()}";
                if (!claves.Add(clave))
                    continue;

                resultado.Add(new ResenhaWeb
                {
                    IdResenha = r.IdResenha,
                    IdFuente = r.IdFuente,
                    IdProducto = r.IdProducto,
                    Calificacion = r.Calificacion,
                    Comentario = r.Comentario.Trim(),
                    FechaResenha = r.FechaResenha
                });
            }
            return resultado;
        }

        private List<Venta> LimpiarVentas(List<Venta> ventas) 
        {
            var resultado = new List<Venta>();
            var claves = new HashSet<string>();

            foreach (var v in ventas)
            {
                if (v.IdCliente <= 0 || v.IdProducto <= 0 || v.Cantidad <= 0 || v.MontoTotal <= 0)
                    continue;

                var clave = $"{v.IdCliente}-{v.IdProducto}-{v.FechaVenta}";
                if (!claves.Add(clave))
                    continue;

                resultado.Add(new Venta
                {
                    IdVenta = v.IdVenta,
                    IdCliente = v.IdCliente,
                    IdProducto = v.IdProducto,
                    FechaVenta = v.FechaVenta,
                    Cantidad = v.Cantidad,
                    MontoTotal = v.MontoTotal
                });
            }
            return resultado;
        }

        private List<Opinion> LimpiarOpiniones(List<Opinion> opiniones) 
        {
            var resultado = new List<Opinion>();
            var claves = new HashSet<string>();

            foreach (var o in opiniones)
            {
                if (o.IdCliente <= 0 || o.IdProducto <= 0 ||
                    o.Calificacion < 1 || o.Calificacion > 5 ||
                    string.IsNullOrWhiteSpace(o.Comentario) ||
                    o.FechaOpinion == default)
                    continue;

                var clave = $"{o.IdCliente}-{o.IdProducto}-{o.Comentario.Trim().ToLower()}";
                if (!claves.Add(clave))
                    continue;

                resultado.Add(new Opinion
                {
                    IdOpinion = o.IdOpinion,
                    IdCliente = o.IdCliente,
                    IdProducto = o.IdProducto,
                    Calificacion = o.Calificacion,
                    Comentario = o.Comentario.Trim(),
                    FechaOpinion = o.FechaOpinion
                });
            }
            return resultado;
        }

        private List<T> LeerCsv<T>(string fileName)
        {
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                IgnoreBlankLines = true
            });

            csv.Context.TypeConverterOptionsCache.GetOptions<int>().NullValues.Add("");
            csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().NullValues.Add("");

            return csv.GetRecords<T>().Where(r => r != null).ToList();
        }
    }
}
