using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class ResenhaWebRepository
    {
        private readonly DbContext _db;

        public ResenhaWebRepository(DbContext db) { _db = db; }

        public void Insertar(List<ResenhaWeb> resenhas)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var r in resenhas)
            {
                var cmd = new SqlCommand(@"INSERT INTO ResenhasWeb (IdResenha, IdFuente, IdProducto, Calificacion, Comentario, FechaResenha) VALUES (@IdResenha, @IdFuente, @IdProducto, @Calificacion, @Comentario, @FechaResenha)", conn);
                cmd.Parameters.AddWithValue("@IdResenha", r.IdResenha);
                cmd.Parameters.AddWithValue("@IdFuente", r.IdFuente);
                cmd.Parameters.AddWithValue("@IdProducto", r.IdProducto);
                cmd.Parameters.AddWithValue("@Calificacion", r.Calificacion);
                cmd.Parameters.AddWithValue("@Comentario", r.Comentario);
                cmd.Parameters.AddWithValue("@FechaResenha", r.FechaResenha);
                cmd.ExecuteNonQuery();
            }
        }
    }
}