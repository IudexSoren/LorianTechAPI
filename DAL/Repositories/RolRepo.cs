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
    public class RolRepo
    {
        public async Task<bool> Create(Rol newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateRol", (SqlConnection)db.Connection);
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
            SqlCommand command = new SqlCommand("spDeleteRol", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Rol> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Rol rol = null;
            SqlCommand command = new SqlCommand("spReadRol", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                rol = new Rol
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                };
            }
            await reader.CloseAsync();

            return rol;
        }

        public async Task<List<Rol>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Rol> roles = new List<Rol>();
            SqlCommand command = new SqlCommand("spReadAllRol", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Rol rol = new Rol
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                };
                roles.Add(rol);
            }
            await reader.CloseAsync();

            return roles;

        }

        public async Task<bool> Update(int id, Rol updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateRol", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
