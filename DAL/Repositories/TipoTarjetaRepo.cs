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
    public class TipoTarjetaRepo
    {
        public async Task<bool> Create(TipoTarjeta newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateTipoTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            command.Parameters.AddWithValue("@Imagen", newObject.RutaImagen);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteTipoTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<TipoTarjeta> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            TipoTarjeta tipo = null;
            SqlCommand command = new SqlCommand("spReadTipoTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                tipo = new TipoTarjeta
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    CreationDate = (DateTime)reader.GetValue(3),
                    ModifiedDate = (DateTime)reader.GetValue(4),
                };
            }
            await reader.CloseAsync();

            return tipo;
        }

        public async Task<List<TipoTarjeta>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<TipoTarjeta> tipos = new List<TipoTarjeta>();
            SqlCommand command = new SqlCommand("spReadAllTipoTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                TipoTarjeta tipo = new TipoTarjeta
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    CreationDate = (DateTime)reader.GetValue(3),
                    ModifiedDate = (DateTime)reader.GetValue(4),
                };
                tipos.Add(tipo);
            }
            await reader.CloseAsync();

            return tipos;

        }

        public async Task<bool> Update(int id, TipoTarjeta updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateTipoTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@Imagen", updatedObject.RutaImagen);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
