using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class ClienteRepository
    {
        private readonly DbContext _db;

        public ClienteRepository(DbContext db) { _db = db; }

        public void Insertar(List<Cliente> clientes)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var c in clientes)
            {
                var cmd = new SqlCommand(@"INSERT INTO Clientes (IdCliente, Nombre, Apellido, Correo, Telefono, FechaRegistro) VALUES (@IdCliente, @Nombre, @Apellido, @Correo, @Telefono, @FechaRegistro)", conn);
                cmd.Parameters.AddWithValue("@IdCliente", c.IdCliente);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Correo", c.Correo);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@FechaRegistro", c.FechaRegistro);
                cmd.ExecuteNonQuery();
            }
        }
    }
}