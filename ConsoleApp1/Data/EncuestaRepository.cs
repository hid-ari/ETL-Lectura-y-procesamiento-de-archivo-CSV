using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class EncuestaRepository
    {
        private readonly DbContext _db;

        public EncuestaRepository(DbContext db) { _db = db; }

        public void Insertar(List<Encuesta> encuestas)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var e in encuestas)
            {
                var cmd = new SqlCommand(@"INSERT INTO Encuestas (IdEncuesta, IdCliente, Pregunta, Respuesta, FechaEncuesta) VALUES (@IdEncuesta, @IdCliente, @Pregunta, @Respuesta, @FechaEncuesta)", conn);
                cmd.Parameters.AddWithValue("@IdEncuesta", e.IdCliente);
                cmd.Parameters.AddWithValue("@IdCliente", e.IdCliente);
                cmd.Parameters.AddWithValue("@Pregunta", e.Pregunta);
                cmd.Parameters.AddWithValue("@Respuesta", e.Respuesta);
                cmd.Parameters.AddWithValue("@FechaEncuesta", e.FechaEncuesta);
                cmd.ExecuteNonQuery();
            }
        }
    }
}