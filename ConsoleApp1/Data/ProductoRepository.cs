using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class ProductoRepository
    {
        private readonly DbContext _db;

        public ProductoRepository(DbContext db) { _db = db; }

        public void Insertar(List<Producto> productos)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var p in productos)
            {
                var cmd = new SqlCommand(@"INSERT INTO Productos (IdProducto, NombreProducto, Descripcion, Categoria, Precio) VALUES (@IdProducto, @NombreProducto, @Descripcion, @Categoria, @Precio)", conn);
                cmd.Parameters.AddWithValue("@IdProducto", p.IdProducto);
                cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
                cmd.Parameters.AddWithValue("@Categoria", p.Categoria);
                cmd.Parameters.AddWithValue("@Precio", p.Precio);
                cmd.ExecuteNonQuery();
            }
        }
    }
}