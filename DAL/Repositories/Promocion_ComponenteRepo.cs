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
    public class Promocion_ComponenteRepo
    {
        public async Task<bool> Create(Promocion_Componente newObject, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spCreatePromocion_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@IdPromocion", newObject.IdPromocion);
            command.Parameters.AddWithValue("@IdComponente", newObject.IdComponente);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spDeletePromocion_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@Id", id);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Promocion_Componente> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Promocion_Componente promocion = null;
            SqlCommand command = new SqlCommand("spReadPromocion_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                promocion = new Promocion_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdPromocion = (int)reader.GetValue(2),
                };
            }
            await reader.CloseAsync();

            return promocion;
        }

        public async Task<Promocion_Componente> ReadByIds(int idComponente, int idPromocion)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Promocion_Componente promocion = null;
            SqlCommand command = new SqlCommand("spReadPromocion_ComponenteByIds", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdComponente", idComponente);
            command.Parameters.AddWithValue("@IdPromocion", idPromocion);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                promocion = new Promocion_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdPromocion = (int)reader.GetValue(2),
                };
            }
            await reader.CloseAsync();

            return promocion;
        }

        public async Task<List<Promocion_Componente>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Promocion_Componente> promociones = new List<Promocion_Componente>();
            SqlCommand command = new SqlCommand("spReadAllPromocion_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Promocion_Componente promocion = new Promocion_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdPromocion = (int)reader.GetValue(2),
                };
                promociones.Add(promocion);
            }
            await reader.CloseAsync();

            return promociones;
        }

        public async Task<List<Promocion_Componente>> ReadAllByIdPromocion(int idPromocion)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Promocion_Componente> promociones = new List<Promocion_Componente>();
            SqlCommand command = new SqlCommand("spReadAllPromocion_ComponenteByIdPromocion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdPromocion", idPromocion);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Promocion_Componente promocion = new Promocion_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdPromocion = (int)reader.GetValue(2),
                };
                promociones.Add(promocion);
            }
            await reader.CloseAsync();

            return promociones;
        }

        public async Task<List<Promocion_Componente>> ReadAllByIdComponente(int idComponente)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Promocion_Componente> promociones = new List<Promocion_Componente>();
            SqlCommand command = new SqlCommand("spReadAllPromocion_ComponenteByIdComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdComponente", idComponente);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Promocion_Componente promocion = new Promocion_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdPromocion = (int)reader.GetValue(2),
                };
                promociones.Add(promocion);
            }
            await reader.CloseAsync();

            return promociones;
        }

        public async Task<bool> Update(int id, Promocion_Componente updatedObject, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spUpdatePromocion_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@IdPromocion", updatedObject.IdPromocion);
            command.Parameters.AddWithValue("@IdComponente", updatedObject.IdComponente);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
