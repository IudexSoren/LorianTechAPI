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
    public class CaracteristicaRepo
    {
        public async Task<bool> Create(Caracteristica newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateCaracteristica", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            command.Parameters.AddWithValue("@Valor", newObject.Valor);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteCaracteristica", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Caracteristica> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Caracteristica caracteristica = null;
            SqlCommand command = new SqlCommand("spReadCaracteristica", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                caracteristica = new Caracteristica
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    Valor = (string)reader.GetValue(2),
                    CreationDate = (DateTime)reader.GetValue(3),
                    ModifiedDate = (DateTime)reader.GetValue(4),
                };
            }
            await reader.CloseAsync();

            return caracteristica;
        }

        public async Task<List<Caracteristica>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Caracteristica> caracteristicas = new List<Caracteristica>();
            SqlCommand command = new SqlCommand("spReadAllCaracteristica", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Caracteristica caracteristica = new Caracteristica
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    Valor = (string)reader.GetValue(2),
                    CreationDate = (DateTime)reader.GetValue(3),
                    ModifiedDate = (DateTime)reader.GetValue(4),
                };
                caracteristicas.Add(caracteristica);
            }
            await reader.CloseAsync();

            return caracteristicas;

        }

        public async Task<bool> Update(int id, Caracteristica updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateCaracteristica", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@Valor", updatedObject.Valor);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
