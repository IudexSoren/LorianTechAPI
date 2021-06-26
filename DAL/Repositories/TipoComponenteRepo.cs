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
    public class TipoComponenteRepo
    {
        public async Task<bool> Create(TipoComponente newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateTipoComponente", (SqlConnection)db.Connection);
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
            SqlCommand command = new SqlCommand("spDeleteTipoComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<TipoComponente> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            TipoComponente tipoComponente = null;
            SqlCommand command = new SqlCommand("spReadTipoComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                tipoComponente = new TipoComponente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3)
                };
            }
            await reader.CloseAsync();

            return tipoComponente;
        }

        public async Task<List<TipoComponente>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<TipoComponente> tipos = new List<TipoComponente>();
            SqlCommand command = new SqlCommand("spReadAllTipoComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                TipoComponente estado = new TipoComponente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3)
                };
                tipos.Add(estado);
            }
            await reader.CloseAsync();

            return tipos;
        }

        public async Task<bool> Update(int id, TipoComponente updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateTipoComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
