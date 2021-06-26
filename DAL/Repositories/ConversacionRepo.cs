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
    public class ConversacionRepo
    {
        public async Task<bool> Create(Conversacion newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateConversacion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteConversacion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Conversacion> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Conversacion conversacion = null;
            SqlCommand command = new SqlCommand("spReadConversacion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                conversacion = new Conversacion
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                };
            }
            await reader.CloseAsync();

            return conversacion;
        }

        public async Task<List<Conversacion>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Conversacion> conversaciones = new List<Conversacion>();
            SqlCommand command = new SqlCommand("spReadAllConversacion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Conversacion conversacion = new Conversacion
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                };
                conversaciones.Add(conversacion);
            }
            await reader.CloseAsync();

            return conversaciones;

        }

        public async Task<bool> Update(int id, Conversacion updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateConversacion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
