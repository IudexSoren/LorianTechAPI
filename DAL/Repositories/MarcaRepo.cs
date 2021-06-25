using DAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Database;
using ENTITIES.Entities;
using System.Data;

namespace DAL.Repositories
{
    public class MarcaRepo
    {
        public async Task<bool> Create(Marca newObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spCreateMarca", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
                command.Parameters.AddWithValue("@Imagen", newObject.RutaImagen);
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
                SqlCommand command = new SqlCommand("spDeleteMarca", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<Marca> Read(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                Marca marca = null;
                SqlCommand command = new SqlCommand("spReadMarca", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    marca = new Marca
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1),
                        RutaImagen = (string)reader.GetValue(2),
                        CreationDate = (DateTime)reader.GetValue(3),
                        ModifiedDate = (DateTime)reader.GetValue(4),
                    };
                }
                await reader.CloseAsync();

                return marca;
            }
        }

        public async Task<List<Marca>> ReadAll()
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                List<Marca> marcas = new List<Marca>();
                SqlCommand command = new SqlCommand("spReadAllMarca", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Marca marca = new Marca
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1),
                        RutaImagen = (string)reader.GetValue(2),
                        CreationDate = (DateTime)reader.GetValue(3),
                        ModifiedDate = (DateTime)reader.GetValue(4),
                    };
                    marcas.Add(marca);
                }
                await reader.CloseAsync();

                return marcas;
            }

        }

        public async Task<bool> Update(int id, Marca updatedObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spUpdateMarca", (SqlConnection)db.Connection);
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
}
