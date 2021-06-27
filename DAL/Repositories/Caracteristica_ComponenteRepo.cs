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
    public class Caracteristica_ComponenteRepo
    {
        public async Task<bool> Create(Caracteristica_Componente newObject, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spCreateCaracteristica_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@IdCaracteristica", newObject.IdCaracteristica);
            command.Parameters.AddWithValue("@IdComponente", newObject.IdComponente);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spDeleteCaracteristica_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@Id", id);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Caracteristica_Componente> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Caracteristica_Componente caracteristica = null;
            SqlCommand command = new SqlCommand("spReadCaracteristica_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                caracteristica = new Caracteristica_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdCaracteristica = (int)reader.GetValue(2),
                };
            }
            await reader.CloseAsync();

            return caracteristica;
        }

        public async Task<Caracteristica_Componente> ReadByIds(int idComponente, int idCaracteristica)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Caracteristica_Componente caracteristica = null;
            SqlCommand command = new SqlCommand("spReadCaracteristica_ComponenteByIds", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdComponente", idComponente);
            command.Parameters.AddWithValue("@IdCaracteristica", idCaracteristica);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                caracteristica = new Caracteristica_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdCaracteristica = (int)reader.GetValue(2),
                };
            }
            await reader.CloseAsync();

            return caracteristica;
        }

        public async Task<List<Caracteristica_Componente>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Caracteristica_Componente> caracteristicas = new List<Caracteristica_Componente>();
            SqlCommand command = new SqlCommand("spReadAllCaracteristica_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Caracteristica_Componente caracteristica = new Caracteristica_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdCaracteristica = (int)reader.GetValue(2),
                };
                caracteristicas.Add(caracteristica);
            }
            await reader.CloseAsync();

            return caracteristicas;
        }

        public async Task<List<Caracteristica_Componente>> ReadAllByIdCaracteristica(int idCaracteristica)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Caracteristica_Componente> caracteristicas = new List<Caracteristica_Componente>();
            SqlCommand command = new SqlCommand("spReadAllCaracteristica_ComponenteByIdCaracteristica", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdCaracteristica", idCaracteristica);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Caracteristica_Componente caracteristica = new Caracteristica_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdCaracteristica = (int)reader.GetValue(2),
                };
                caracteristicas.Add(caracteristica);
            }
            await reader.CloseAsync();

            return caracteristicas;
        }

        public async Task<List<Caracteristica_Componente>> ReadAllByIdComponente(int idComponente)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Caracteristica_Componente> caracteristicas = new List<Caracteristica_Componente>();
            SqlCommand command = new SqlCommand("spReadAllCaracteristica_ComponenteByIdComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdComponente", idComponente);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Caracteristica_Componente caracteristica = new Caracteristica_Componente
                {
                    Id = (int)reader.GetValue(0),
                    IdComponente = (int)reader.GetValue(1),
                    IdCaracteristica = (int)reader.GetValue(2),
                };
                caracteristicas.Add(caracteristica);
            }
            await reader.CloseAsync();

            return caracteristicas;
        }

        public async Task<bool> Update(int id, Caracteristica_Componente updatedObject, IDataBase db, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand("spUpdateCaracteristica_Componente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@IdCaracteristica", updatedObject.IdCaracteristica);
            command.Parameters.AddWithValue("@IdComponente", updatedObject.IdComponente);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
