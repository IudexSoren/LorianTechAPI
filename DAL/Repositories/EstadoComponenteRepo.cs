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
    public class EstadoComponenteRepo
    {
        public async Task<bool> Create(EstadoComponente newObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spCreateEstadoComponente", (SqlConnection)db.Connection);
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
                SqlCommand command = new SqlCommand("spDeleteEstadoComponente", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }

        public async Task<EstadoComponente> Read(int id)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                EstadoComponente estadoComponente = null;
                SqlCommand command = new SqlCommand("spReadEstadoComponente", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    estadoComponente = new EstadoComponente
                    {
                        Id = (int)reader.GetValue(0),
                        Nombre = (string)reader.GetValue(1)
                    };
                }
                await reader.CloseAsync();

                return estadoComponente;
            }
        }

        public async Task<List<EstadoComponente>> ReadAll()
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                List<EstadoComponente> estados = new List<EstadoComponente>();
                SqlCommand command = new SqlCommand("spReadAllEstadoComponente", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    EstadoComponente estado = new EstadoComponente
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

        public async Task<bool> Update(int id, EstadoComponente updatedObject)
        {
            using (IDataBase db = FactoryDatabase.CreateDatabaseObject())
            {
                SqlCommand command = new SqlCommand("spUpdateEstadoComponente", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
                bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

                return state;
            }
        }
    }
}
