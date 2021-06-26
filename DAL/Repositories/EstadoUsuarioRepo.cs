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
    public class EstadoUsuarioRepo
    {
        public async Task<bool> Create(EstadoUsuario newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateEstadoUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteEstadoUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<EstadoUsuario> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            EstadoUsuario estadoUsuario = null;
            SqlCommand command = new SqlCommand("spReadEstadoUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                estadoUsuario = new EstadoUsuario
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1)
                };
            }
            await reader.CloseAsync();

            return estadoUsuario;
        }

        public async Task<List<EstadoUsuario>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<EstadoUsuario> estados = new List<EstadoUsuario>();
            SqlCommand command = new SqlCommand("spReadAllEstadoUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                EstadoUsuario estado = new EstadoUsuario
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1)
                };
                estados.Add(estado);
            }
            await reader.CloseAsync();

            return estados;
        }

        public async Task<bool> Update(int id, EstadoUsuario updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateEstadoUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
