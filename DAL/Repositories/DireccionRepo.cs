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
    public class DireccionRepo
    {
        public async Task<bool> Create(Direccion newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateDireccion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Descripcion", newObject.Descripcion);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", newObject.IdUsuario);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteDireccion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Direccion> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Direccion direccion = null;
            SqlCommand command = new SqlCommand("spReadDireccion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                direccion = new Direccion
                {
                    Id = (int)reader.GetValue(0),
                    Descripcion = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                    IdUsuario = (int)reader.GetValue(4)
                };
            }
            await reader.CloseAsync();

            return direccion;
        }

        public async Task<List<Direccion>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Direccion> direcciones = new List<Direccion>();
            SqlCommand command = new SqlCommand("spReadAllDireccion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Direccion direccion = new Direccion
                {
                    Id = (int)reader.GetValue(0),
                    Descripcion = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                    IdUsuario = (int)reader.GetValue(4)
                };
                direcciones.Add(direccion);
            }
            await reader.CloseAsync();

            return direcciones;
        }

        public async Task<List<Direccion>> ReadAllByIdUsuario(int idUsuario)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Direccion> direcciones = new List<Direccion>();
            SqlCommand command = new SqlCommand("spReadAllDireccionByIdUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Direccion direccion = new Direccion
                {
                    Id = (int)reader.GetValue(0),
                    Descripcion = (string)reader.GetValue(1),
                    CreationDate = (DateTime)reader.GetValue(2),
                    ModifiedDate = (DateTime)reader.GetValue(3),
                    IdUsuario = (int)reader.GetValue(4)
                };
                direcciones.Add(direccion);
            }
            await reader.CloseAsync();

            return direcciones;
        }

        public async Task<bool> Update(int id, Direccion updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateDireccion", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Descripcion", updatedObject.Descripcion);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", updatedObject.IdUsuario);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
