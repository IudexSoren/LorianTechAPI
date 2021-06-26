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
    public class ComponenteRepo
    {
        public async Task<bool> Create(Componente newObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spCreateComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", newObject.Nombre);
            command.Parameters.AddWithValue("@Imagen", newObject.RutaImagen);
            command.Parameters.AddWithValue("@Inventario", newObject.Inventario);
            command.Parameters.AddWithValue("@Garantia", newObject.Garantia);
            command.Parameters.AddWithValue("@Precio", newObject.Precio);
            command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
            command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdTipoComponente", newObject.IdTipoComponente);
            command.Parameters.AddWithValue("@IdMarca", newObject.IdMarca);
            command.Parameters.AddWithValue("@IdEstadoComponente", newObject.IdEstadoComponente);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Componente> Read(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            Componente componente = null;
            SqlCommand command = new SqlCommand("spReadComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                componente = new Componente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    Inventario = (int)reader.GetValue(3),
                    Garantia = (string)reader.GetValue(4),
                    Precio = (decimal)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdTipoComponente = (int)reader.GetValue(8),
                    IdMarca = (int)reader.GetValue(9),
                    IdEstadoComponente = (int)reader.GetValue(10),
                };
            }
            await reader.CloseAsync();

            return componente;
        }

        public async Task<List<Componente>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Componente> componentes = new List<Componente>();
            SqlCommand command = new SqlCommand("spReadAllComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Componente componente = new Componente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    Inventario = (int)reader.GetValue(3),
                    Garantia = (string)reader.GetValue(4),
                    Precio = (decimal)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdTipoComponente = (int)reader.GetValue(8),
                    IdMarca = (int)reader.GetValue(9),
                    IdEstadoComponente = (int)reader.GetValue(10),
                };
                componentes.Add(componente);
            }
            await reader.CloseAsync();

            return componentes;
        }

        public async Task<List<Componente>> ReadAllByIdTipoComponente(int idTipoComponente)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Componente> componentes = new List<Componente>();
            SqlCommand command = new SqlCommand("spReadAllComponenteByIdTipoComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdTipoComponente", idTipoComponente);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Componente componente = new Componente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    Inventario = (int)reader.GetValue(3),
                    Garantia = (string)reader.GetValue(4),
                    Precio = (decimal)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdTipoComponente = (int)reader.GetValue(8),
                    IdMarca = (int)reader.GetValue(9),
                    IdEstadoComponente = (int)reader.GetValue(10),
                };
                componentes.Add(componente);
            }
            await reader.CloseAsync();

            return componentes;
        }

        public async Task<List<Componente>> ReadAllByIdMarca(int idMarca)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Componente> componentes = new List<Componente>();
            SqlCommand command = new SqlCommand("spReadAllComponenteByIdMarca", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdMarca", idMarca);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Componente componente = new Componente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    Inventario = (int)reader.GetValue(3),
                    Garantia = (string)reader.GetValue(4),
                    Precio = (decimal)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdTipoComponente = (int)reader.GetValue(8),
                    IdMarca = (int)reader.GetValue(9),
                    IdEstadoComponente = (int)reader.GetValue(10),
                };
                componentes.Add(componente);
            }
            await reader.CloseAsync();

            return componentes;
        }

        public async Task<List<Componente>> ReadAllByIdEstadoComponente(int idEstadoComponente)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Componente> componentes = new List<Componente>();
            SqlCommand command = new SqlCommand("spReadAllComponenteByIdEstadoComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdEstadoComponente", idEstadoComponente);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Componente componente = new Componente
                {
                    Id = (int)reader.GetValue(0),
                    Nombre = (string)reader.GetValue(1),
                    RutaImagen = (string)reader.GetValue(2),
                    Inventario = (int)reader.GetValue(3),
                    Garantia = (string)reader.GetValue(4),
                    Precio = (decimal)reader.GetValue(5),
                    CreationDate = (DateTime)reader.GetValue(6),
                    ModifiedDate = (DateTime)reader.GetValue(7),
                    IdTipoComponente = (int)reader.GetValue(8),
                    IdMarca = (int)reader.GetValue(9),
                    IdEstadoComponente = (int)reader.GetValue(10),
                };
                componentes.Add(componente);
            }
            await reader.CloseAsync();

            return componentes;
        }

        public async Task<bool> Update(int id, Componente updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateComponente", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nombre", updatedObject.Nombre);
            command.Parameters.AddWithValue("@Imagen", updatedObject.RutaImagen);
            command.Parameters.AddWithValue("@Inventario", updatedObject.Inventario);
            command.Parameters.AddWithValue("@Garantia", updatedObject.Garantia);
            command.Parameters.AddWithValue("@Precio", updatedObject.Precio);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdTipoComponente", updatedObject.IdTipoComponente);
            command.Parameters.AddWithValue("@IdMarca", updatedObject.IdMarca);
            command.Parameters.AddWithValue("@IdEstadoComponente", updatedObject.IdEstadoComponente);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
