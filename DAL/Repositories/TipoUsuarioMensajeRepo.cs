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
    public class TipoUsuarioMensajeRepo
    {
        public async Task<bool> Create(TipoUsuarioMensaje newObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spCreateTipoUsuarioMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spDeleteTipoUsuarioMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<TipoUsuarioMensaje> Read(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                TipoUsuarioMensaje tipoUsuarioMensaje = null;
                SqlCommand command = new SqlCommand("spReadTipoUsuarioMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    tipoUsuarioMensaje = new TipoUsuarioMensaje
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1),
                    };
                }
                await reader.CloseAsync();

                return tipoUsuarioMensaje;
            }
        }

        public async Task<List<TipoUsuarioMensaje>> ReadAll()
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                List<TipoUsuarioMensaje> tipos = new List<TipoUsuarioMensaje>();
                SqlCommand command = new SqlCommand("spReadAllTipoUsuarioMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    TipoUsuarioMensaje estado = new TipoUsuarioMensaje
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1),
                    };
                    tipos.Add(estado);
                }
                await reader.CloseAsync();

                return tipos;
            }
        }

        public async Task<bool> Update(int id, TipoUsuarioMensaje updatedObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spUpdateTipoUsuarioMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }
    }
}
