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
    public class PromocionRepo
    {
        public async Task<bool> Create(Promocion newObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spCreatePromocion", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
                command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
                command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spDeletePromocion", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<Promocion> Read(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                Promocion promocion = null;
                SqlCommand command = new SqlCommand("spReadPromocion", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    promocion = new Promocion
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1),
                        CreationDate = (DateTime)reader.GetValue(2),
                        ModifiedDate = (DateTime)reader.GetValue(3),
                    };
                }
                await reader.CloseAsync();

                return promocion;
            }
        }

        public async Task<List<Promocion>> ReadAll()
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                List<Promocion> promociones = new List<Promocion>();
                SqlCommand command = new SqlCommand("spReadAllPromocion", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Promocion promocion = new Promocion
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1),
                        CreationDate = (DateTime)reader.GetValue(2),
                        ModifiedDate = (DateTime)reader.GetValue(3),
                    };
                    promociones.Add(promocion);
                }
                await reader.CloseAsync();

                return promociones;
            }

        }

        public async Task<bool> Update(int id, Promocion updatedObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spUpdatePromocion", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
                command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }
    }
}
