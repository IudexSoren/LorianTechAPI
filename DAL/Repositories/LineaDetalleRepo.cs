using DAL.Database;
using DAL.Interfaces;
using ENTITIES.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LineaDetalleRepo
    {
        public async Task<bool> Create(LineaDetalle newObject, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spCreateLineaDetalle", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@Cantidad", newObject.Cantidad);
            command.Parameters.AddWithValue("@Total", newObject.Total);
            command.Parameters.AddWithValue("@IdFactura", newObject.IdFactura);
            command.Parameters.AddWithValue("@IdComponente", newObject.IdComponente);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spDeleteLineaDetalle", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@Id", id);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<LineaDetalle> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            LineaDetalle linea = null;
            SqlCommand command = new SqlCommand("spReadLineaDetalle", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                linea = new LineaDetalle
                {
                    Id = (int)reader.GetValue(0),
                    Cantidad = (int)reader.GetValue(1),
                    Total = (decimal)reader.GetValue(2),
                    IdComponente = (int)reader.GetValue(3),
                    IdFactura = (int)reader.GetValue(4),
                };
            }
            await reader.CloseAsync();

            return linea;
        }

        public async Task<LineaDetalle> ReadByIds(int idComponente, int idFactura)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            LineaDetalle linea = null;
            SqlCommand command = new SqlCommand("spReadLineaDetalleByIds", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdComponente", idComponente);
            command.Parameters.AddWithValue("@IdFactura", idFactura);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                linea = new LineaDetalle
                {
                    Id = (int)reader.GetValue(0),
                    Cantidad = (int)reader.GetValue(1),
                    Total = (decimal)reader.GetValue(2),
                    IdComponente = (int)reader.GetValue(3),
                    IdFactura = (int)reader.GetValue(4),
                };
            }
            await reader.CloseAsync();

            return linea;
        }

        public async Task<List<LineaDetalle>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<LineaDetalle> lineas = new List<LineaDetalle>();
            SqlCommand command = new SqlCommand("spReadAllLineaDetalle", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                LineaDetalle linea = new LineaDetalle
                {
                    Id = (int)reader.GetValue(0),
                    Cantidad = (int)reader.GetValue(1),
                    Total = (decimal)reader.GetValue(2),
                    IdComponente = (int)reader.GetValue(3),
                    IdFactura = (int)reader.GetValue(4),
                };
                lineas.Add(linea);
            }
            await reader.CloseAsync();

            return lineas;
        }

        public async Task<List<LineaDetalle>> ReadAllByIdFactura(int idFactura, IDataBase db, SqlTransaction transaction)
        {
            List<LineaDetalle> lineas = new List<LineaDetalle>();
            SqlCommand command = new SqlCommand("spReadAllLineaDetalleByIdFactura", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;
            command.Parameters.AddWithValue("@IdFactura", idFactura);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                LineaDetalle linea = new LineaDetalle
                {
                    Id = (int)reader.GetValue(0),
                    Cantidad = (int)reader.GetValue(1),
                    Total = (decimal)reader.GetValue(2),
                    IdComponente = (int)reader.GetValue(3),
                    IdFactura = (int)reader.GetValue(4),
                };
                lineas.Add(linea);
            }
            await reader.CloseAsync();

            return lineas;
        }

        public async Task<List<LineaDetalle>> ReadAllByIdComponente(int idComponente)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<LineaDetalle> lineas = new List<LineaDetalle>();
            SqlCommand command = new SqlCommand("spReadAllLineaDetalleByIdComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdComponente", idComponente);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                LineaDetalle linea = new LineaDetalle
                {
                    Id = (int)reader.GetValue(0),
                    Cantidad = (int)reader.GetValue(1),
                    Total = (decimal)reader.GetValue(2),
                    IdComponente = (int)reader.GetValue(3),
                    IdFactura = (int)reader.GetValue(4),
                };
                lineas.Add(linea);
            }
            await reader.CloseAsync();

            return lineas;
        }
    }
}
