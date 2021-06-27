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
    public class UsuarioRepo
    {
        public async Task<bool> Create(Usuario newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Email", newObject.Email);
            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            command.Parameters.AddWithValue("@Apellido", newObject.Apellido);
            command.Parameters.AddWithValue("@Clave", newObject.Clave);
            command.Parameters.AddWithValue("@EmailVerificado", newObject.EmailVerificado);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdEstadoUsuario", newObject.IdEstadoUsuario);
            command.Parameters.AddWithValue("@IdRol", newObject.IdRol);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Usuario> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Usuario usuario = null;
            SqlCommand command = new SqlCommand("spReadUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                usuario = new Usuario
                {
                    Id = (int)reader.GetValue(0),
                    Email = (string)reader.GetValue(1),
                    Nombre = (string)reader.GetValue(2),
                    Apellido = (string)reader.GetValue(3),
                    Clave = (string)reader.GetValue(4),
                    EmailVerificado = (bool)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdEstadoUsuario = (int)reader.GetValue(8),
                    IdRol = (int)reader.GetValue(9),
                };
            }
            await reader.CloseAsync();

            return usuario;
        }

        public async Task<List<Usuario>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command = new SqlCommand("spReadAllUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Usuario usuario = new Usuario
                {
                    Id = (int)reader.GetValue(0),
                    Email = (string)reader.GetValue(1),
                    Nombre = (string)reader.GetValue(2),
                    Apellido = (string)reader.GetValue(3),
                    Clave = (string)reader.GetValue(4),
                    EmailVerificado = (bool)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdEstadoUsuario = (int)reader.GetValue(8),
                    IdRol = (int)reader.GetValue(9),
                };
                usuarios.Add(usuario);
            }
            await reader.CloseAsync();

            return usuarios;

        }

        public async Task<bool> Update(int id, Usuario updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Email", updatedObject.Email);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@Apellido", updatedObject.Apellido);
            command.Parameters.AddWithValue("@Clave", updatedObject.Clave);
            command.Parameters.AddWithValue("@EmailVerificado", updatedObject.EmailVerificado);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdEstadoUsuario", updatedObject.IdEstadoUsuario);
            command.Parameters.AddWithValue("@IdRol", updatedObject.IdRol);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
