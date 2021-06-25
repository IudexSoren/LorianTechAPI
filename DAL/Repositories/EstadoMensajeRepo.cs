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
    public class EstadoMensajeRepo
    {
        public async Task<bool> Create(EstadoMensaje newObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spCreateEstadoMensaje", (SqlConnection)db.Connection);
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
                SqlCommand command = new SqlCommand("spDeleteEstadoMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<EstadoMensaje> Read(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                EstadoMensaje estadoMensaje = null;
                SqlCommand command = new SqlCommand("spReadEstadoMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    estadoMensaje = new EstadoMensaje
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1)
                    };
                }
                await reader.CloseAsync();

                return estadoMensaje;
            }
        }

        public async Task<List<EstadoMensaje>> ReadAll()
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                List<EstadoMensaje> estados = new List<EstadoMensaje>();
                SqlCommand command = new SqlCommand("spReadAllEstadoMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    EstadoMensaje estado = new EstadoMensaje
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1)
                    };
                    estados.Add(estado);
                }
                await reader.CloseAsync();

                return estados;
            }
        }

        public async Task<bool> Update(int id, EstadoMensaje updatedObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spUpdateEstadoMensaje", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }
    }
}
