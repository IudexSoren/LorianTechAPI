using DAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Database;
using ENTITIES.Entities;
using System.Data;
using System.Runtime.CompilerServices;

namespace DAL.Repositories
{
    public class EstadoEnvioRepo
    {
        public async Task<bool> Create(EstadoEnvio newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateEstadoEnvio", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteEstadoEnvio", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<EstadoEnvio> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            EstadoEnvio estadoEnvio = null;
            SqlCommand command = new SqlCommand("spReadEstadoEnvio", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                estadoEnvio = new EstadoEnvio
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1)
                };
            }
            await reader.CloseAsync();

            return estadoEnvio;
        }

        public async Task<List<EstadoEnvio>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<EstadoEnvio> estados = new List<EstadoEnvio>();
            SqlCommand command = new SqlCommand("spReadAllEstadoEnvio", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                EstadoEnvio estado = new EstadoEnvio
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1)
                };
                estados.Add(estado);
            }
            await reader.CloseAsync();

            return estados;
        }

        public async Task<bool> Update(int id, EstadoEnvio updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateEstadoEnvio", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
