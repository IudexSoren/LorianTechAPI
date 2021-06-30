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
    public class TelefonoRepo
    {
        public async Task<bool> Create(Telefono newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateTelefono", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Numero", newObject.Numero);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", newObject.IdUsuario);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteTelefono", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Telefono> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Telefono telefono = null;
            SqlCommand command = new SqlCommand("spReadTelefono", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                telefono = new Telefono
                {
                    Id = (int)reader.GetValue(0),
                    Numero = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                    IdUsuario = (int)reader.GetValue(4)
                };
            }
            await reader.CloseAsync();

            return telefono;
        }

        public async Task<List<Telefono>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Telefono> telefonos = new List<Telefono>();
            SqlCommand command = new SqlCommand("spReadAllTelefono", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Telefono telefono = new Telefono
                {
                    Id = (int)reader.GetValue(0),
                    Numero = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                    IdUsuario = (int)reader.GetValue(4)
                };
                telefonos.Add(telefono);
            }
            await reader.CloseAsync();

            return telefonos;
        }

        public async Task<List<Telefono>> ReadAllByIdUsuario(int idUsuario)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Telefono> telefonos = new List<Telefono>();
            SqlCommand command = new SqlCommand("spReadAllTelefonoByIdUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Telefono telefono = new Telefono
                {
                    Id = (int)reader.GetValue(0),
                    Numero = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                    IdUsuario = (int)reader.GetValue(4)
                };
                telefonos.Add(telefono);
            }
            await reader.CloseAsync();

            return telefonos;
        }

        public async Task<bool> Update(int id, Telefono updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateTelefono", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Numero", updatedObject.Numero);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", updatedObject.IdUsuario);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
