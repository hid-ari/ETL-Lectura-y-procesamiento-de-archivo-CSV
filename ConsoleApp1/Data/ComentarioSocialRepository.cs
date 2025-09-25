using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class ComentarioSocialRepository
    {
        private readonly DbContext _db;

        public ComentarioSocialRepository(DbContext db) { _db = db; }

        public void Insertar(List<ComentarioSocial> comentarios)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var c in comentarios)
            {
                var cmd = new SqlCommand(@"INSERT INTO ComentariosSociales (IdComentario, IdFuente, IdCliente, TextoComentario, FechaComentario, Reacciones) VALUES (@IdComentario, @IdFuente, @IdCliente, @TextoComentario, @FechaComentario, @Reacciones)", conn);
                cmd.Parameters.AddWithValue("@IdComentario", c.IdComentario);
                cmd.Parameters.AddWithValue("@IdFuente", c.IdFuente);
                cmd.Parameters.AddWithValue("@IdCliente", c.IdCliente);
                cmd.Parameters.AddWithValue("@TextoComentario", c.TextoComentario);
                cmd.Parameters.AddWithValue("@FechaComentario", c.FechaComentario);
                cmd.Parameters.AddWithValue("@Reacciones", c.Reacciones);
                cmd.ExecuteNonQuery();
            }
        }
    }
}