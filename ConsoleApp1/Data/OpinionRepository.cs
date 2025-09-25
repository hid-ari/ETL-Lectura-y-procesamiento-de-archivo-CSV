using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class OpinionRepository
    {
        private readonly DbContext _db;

        public OpinionRepository(DbContext db) { _db = db; }

        public void Insertar(List<Opinion> opiniones)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var o in opiniones)
            {
                var cmd = new SqlCommand(@"
                    INSERT INTO Opiniones (IdOpinion, IdCliente, IdProducto, Calificacion, Comentario, FechaOpinion)
                    VALUES (@IdOpinion, @IdCliente, @IdProducto, @Calificacion, @Comentario, @FechaOpinion)", conn);

                cmd.Parameters.AddWithValue("@IdOpinion", o.IdOpinion);
                cmd.Parameters.AddWithValue("@IdCliente", o.IdCliente);
                cmd.Parameters.AddWithValue("@IdProducto", o.IdProducto);
                cmd.Parameters.AddWithValue("@Calificacion", o.Calificacion);
                cmd.Parameters.AddWithValue("@Comentario", o.Comentario);
                cmd.Parameters.AddWithValue("@FechaOpinion", o.FechaOpinion);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
