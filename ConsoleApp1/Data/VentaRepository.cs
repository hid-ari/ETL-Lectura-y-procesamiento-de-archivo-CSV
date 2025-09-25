using Entities;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class VentaRepository
    {
        private readonly DbContext _db;

        public VentaRepository(DbContext db) { _db = db; }

        public void Insertar(List<Venta> ventas)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            foreach (var v in ventas)
            {
                var cmd = new SqlCommand(@"
                    INSERT INTO Ventas (IdVenta, IdCliente, IdProducto, FechaVenta, Cantidad, MontoTotal)
                    VALUES (@IdVenta, @IdCliente, @IdProducto, @FechaVenta, @Cantidad, @MontoTotal)", conn);

                cmd.Parameters.AddWithValue("@IdVenta", v.IdVenta);
                cmd.Parameters.AddWithValue("@IdCliente", v.IdCliente);
                cmd.Parameters.AddWithValue("@IdProducto", v.IdProducto);
                cmd.Parameters.AddWithValue("@FechaVenta", v.FechaVenta);
                cmd.Parameters.AddWithValue("@Cantidad", v.Cantidad);
                cmd.Parameters.AddWithValue("@MontoTotal", v.MontoTotal);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
