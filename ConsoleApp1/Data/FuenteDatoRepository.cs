using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class FuenteDatoRepository
    {
        private readonly DbContext _db;

        public FuenteDatoRepository(DbContext db) { _db = db; }

        public void Insertar(List<FuenteDato> fuentes)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var f in fuentes)
            {
                var cmd = new SqlCommand(@"INSERT INTO FuentesDatos (IdFuente, NombreFuente, TipoFuente, URL) VALUES (@IdFuente, @NombreFuente, @TipoFuente, @URL)", conn);
                cmd.Parameters.AddWithValue("@IdFuente", f.IdFuente);
                cmd.Parameters.AddWithValue("@NombreFuente", f.NombreFuente);
                cmd.Parameters.AddWithValue("@TipoFuente", f.TipoFuente);
                cmd.Parameters.AddWithValue("@URL", f.URL);
                cmd.ExecuteNonQuery();
            }
        }
    }
}