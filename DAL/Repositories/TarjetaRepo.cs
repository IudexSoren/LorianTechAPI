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
    public class TarjetaRepo
    {
        public async Task<bool> Create(Tarjeta newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Numero", newObject.Numero);
            command.Parameters.AddWithValue("@FechaExpiracion", newObject.FechaExpiracion);
            command.Parameters.AddWithValue("@CCV", newObject.CVV);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", newObject.IdUsuario);
            command.Parameters.AddWithValue("@IdTipoTarjeta", newObject.IdTipoTarjeta);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Tarjeta> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Tarjeta tarjeta = null;
            SqlCommand command = new SqlCommand("spReadTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                tarjeta = new Tarjeta
                {
                    Id = (int)reader.GetValue(0),
                    Numero = (string)reader.GetValue(1),
                    FechaExpiracion = (DateTime)reader.GetValue(2),
                    CVV = (string)reader.GetValue(3),
                    CreationDate = (DateTime)reader.GetValue(4),
                    ModifiedDate = (DateTime)reader.GetValue(5),
                    IdUsuario = (int)reader.GetValue(6),
                    IdTipoTarjeta = (int)reader.GetValue(7)
                };
            }
            await reader.CloseAsync();

            return tarjeta;
        }

        public async Task<List<Tarjeta>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            SqlCommand command = new SqlCommand("spReadAllTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Tarjeta tarjeta = new Tarjeta
                {
                    Id = (int)reader.GetValue(0),
                    Numero = (string)reader.GetValue(1),
                    FechaExpiracion = (DateTime)reader.GetValue(2),
                    CVV = (string)reader.GetValue(3),
                    CreationDate = (DateTime)reader.GetValue(4),
                    ModifiedDate = (DateTime)reader.GetValue(5),
                    IdUsuario = (int)reader.GetValue(6),
                    IdTipoTarjeta = (int)reader.GetValue(7)
                };
                tarjetas.Add(tarjeta);
            }
            await reader.CloseAsync();

            return tarjetas;
        }

        public async Task<List<Tarjeta>> ReadAllByIdUsuario(int idUsuario)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            SqlCommand command = new SqlCommand("spReadAllTarjetaByIdUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Tarjeta tarjeta = new Tarjeta
                {
                    Id = (int)reader.GetValue(0),
                    Numero = (string)reader.GetValue(1),
                    FechaExpiracion = (DateTime)reader.GetValue(2),
                    CVV = (string)reader.GetValue(3),
                    CreationDate = (DateTime)reader.GetValue(4),
                    ModifiedDate = (DateTime)reader.GetValue(5),
                    IdUsuario = (int)reader.GetValue(6),
                    IdTipoTarjeta = (int)reader.GetValue(7)
                };
                tarjetas.Add(tarjeta);
            }
            await reader.CloseAsync();

            return tarjetas;
        }

        public async Task<bool> Update(int id, Tarjeta updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Numero", updatedObject.Numero);
            command.Parameters.AddWithValue("@FechaExpiracion", updatedObject.FechaExpiracion);
            command.Parameters.AddWithValue("@CCV", updatedObject.CVV);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", updatedObject.IdUsuario);
            command.Parameters.AddWithValue("@IdTipoTarjeta", updatedObject.IdTipoTarjeta);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
