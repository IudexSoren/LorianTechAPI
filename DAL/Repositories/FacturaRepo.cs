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
    public class FacturaRepo
    {
        public async Task<bool> Create(Factura newObject)
        {
            bool state;
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlTransaction transaction = (SqlTransaction)db.Connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand("spCreateFactura", (SqlConnection)db.Connection);
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Descuento", newObject.Descuento);
                command.Parameters.AddWithValue("@Impuesto", newObject.Impuesto);
                command.Parameters.AddWithValue("@Subtotal", newObject.Subtotal);
                command.Parameters.AddWithValue("@Total", newObject.Total);
                command.Parameters.AddWithValue("@CreationDate", newObject.CreationDate);
                command.Parameters.AddWithValue("@ModifiedDate", newObject.ModifiedDate);
                command.Parameters.AddWithValue("@IdUsuario", newObject.CreationDate);
                command.Parameters.AddWithValue("@IdTarjeta", newObject.IdTarjeta);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                state = await reader.ReadAsync();
                if (state)
                {
                    newObject.Id = Convert.ToInt32(reader.GetValue(0));
                    await reader.CloseAsync();
                    if (newObject.Detalles != null)
                    {
                        LineaDetalleRepo ccr = new LineaDetalleRepo();
                        foreach (var linea in newObject.Detalles)
                        {
                            linea.IdFactura = newObject.Id;
                            state = await ccr.Create(linea, db, transaction);
                            if (!state)
                            {
                                break;
                            }
                        }
                    }
                }

                if (state)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                state = false;
                try
                {
                    await transaction.RollbackAsync();
                }
                catch (Exception exception2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw exception2;
                }
            }

            return state;
        }

        public async Task<bool> Delete(int id)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spDeleteFactura", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }

        public async Task<Factura> Read(int id)
        {
            Factura factura = null;
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlTransaction transaction = (SqlTransaction)db.Connection.BeginTransaction();
            try
            {
                SqlCommand command = new SqlCommand("spReadFactura", (SqlConnection)db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    factura = new Factura
                    {
                        Id = (int)reader.GetValue(0),
                        Descuento = (decimal)reader.GetValue(1),
                        Impuesto = (decimal)reader.GetValue(2),
                        Subtotal = (decimal)reader.GetValue(3),
                        Total = (decimal)reader.GetValue(4),
                        CreationDate = (DateTime)reader.GetValue(5),
                        ModifiedDate = (DateTime)reader.GetValue(6),
                        IdUsuario = (int)reader.GetValue(7),
                        IdTarjeta = (int)reader.GetValue(8),
                    };
                    await reader.CloseAsync();
                    LineaDetalleRepo ldr = new LineaDetalleRepo();
                    List<LineaDetalle> lineas = await ldr.ReadAllByIdFactura(id, db, transaction);
                    factura.Detalles = lineas;
                }
                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                try
                {
                    await transaction.RollbackAsync();
                }
                catch (Exception exception2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    throw exception2;
                }
            }

            return factura;
        }

        public async Task<List<Factura>> ReadAll()
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Factura> facturas = new List<Factura>();
            SqlCommand command = new SqlCommand("spReadAllFactura", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Factura factura = new Factura
                {
                    Id = (int)reader.GetValue(0),
                    Descuento = (decimal)reader.GetValue(1),
                    Impuesto = (decimal)reader.GetValue(2),
                    Subtotal = (decimal)reader.GetValue(3),
                    Total = (decimal)reader.GetValue(4),
                    CreationDate = (DateTime)reader.GetValue(5),
                    ModifiedDate = (DateTime)reader.GetValue(6),
                    IdUsuario = (int)reader.GetValue(7),
                    IdTarjeta = (int)reader.GetValue(8),
                };
                facturas.Add(factura);
            }
            await reader.CloseAsync();

            return facturas;
        }

        public async Task<List<Factura>> ReadAllByIdTarjeta(int idTarjeta)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Factura> facturas = new List<Factura>();
            SqlCommand command = new SqlCommand("spReadAllFacturaByIdTarjeta", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdTarjeta", idTarjeta);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Factura factura = new Factura
                {
                    Id = (int)reader.GetValue(0),
                    Descuento = (decimal)reader.GetValue(1),
                    Impuesto = (decimal)reader.GetValue(2),
                    Subtotal = (decimal)reader.GetValue(3),
                    Total = (decimal)reader.GetValue(4),
                    CreationDate = (DateTime)reader.GetValue(5),
                    ModifiedDate = (DateTime)reader.GetValue(6),
                    IdUsuario = (int)reader.GetValue(7),
                    IdTarjeta = (int)reader.GetValue(8),
                };
                facturas.Add(factura);
            }
            await reader.CloseAsync();

            return facturas;
        }

        public async Task<List<Factura>> ReadAllByIdUsuario(int idUsuario)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Factura> facturas = new List<Factura>();
            SqlCommand command = new SqlCommand("spReadAllFacturaByIdUsuario", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Factura factura = new Factura
                {
                    Id = (int)reader.GetValue(0),
                    Descuento = (decimal)reader.GetValue(1),
                    Impuesto = (decimal)reader.GetValue(2),
                    Subtotal = (decimal)reader.GetValue(3),
                    Total = (decimal)reader.GetValue(4),
                    CreationDate = (DateTime)reader.GetValue(5),
                    ModifiedDate = (DateTime)reader.GetValue(6),
                    IdUsuario = (int)reader.GetValue(7),
                    IdTarjeta = (int)reader.GetValue(8),
                };
                facturas.Add(factura);
            }
            await reader.CloseAsync();

            return facturas;
        }

        public async Task<List<Factura>> ReadAllByIdEstadoFactura(int idEstadoFactura)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            List<Factura> facturas = new List<Factura>();
            SqlCommand command = new SqlCommand("spReadAllFacturaByIdEstadoFactura", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdEstadoFactura", idEstadoFactura);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Factura factura = new Factura
                {
                    Id = (int)reader.GetValue(0),
                    Descuento = (decimal)reader.GetValue(1),
                    Impuesto = (decimal)reader.GetValue(2),
                    Subtotal = (decimal)reader.GetValue(3),
                    Total = (decimal)reader.GetValue(4),
                    CreationDate = (DateTime)reader.GetValue(5),
                    ModifiedDate = (DateTime)reader.GetValue(6),
                    IdUsuario = (int)reader.GetValue(7),
                    IdTarjeta = (int)reader.GetValue(8),
                };
                facturas.Add(factura);
            }
            await reader.CloseAsync();

            return facturas;
        }

        public async Task<bool> Update(int id, Factura updatedObject)
        {
            using IDataBase db = FactoryDatabase.CreateDatabaseObject();
            SqlCommand command = new SqlCommand("spUpdateFactura", (SqlConnection)db.Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Descuento", updatedObject.Descuento);
            command.Parameters.AddWithValue("@Impuesto", updatedObject.Impuesto);
            command.Parameters.AddWithValue("@Subtotal", updatedObject.Subtotal);
            command.Parameters.AddWithValue("@Total", updatedObject.Total);
            command.Parameters.AddWithValue("@ModifiedDate", updatedObject.ModifiedDate);
            command.Parameters.AddWithValue("@IdUsuario", updatedObject.CreationDate);
            command.Parameters.AddWithValue("@IdTarjeta", updatedObject.IdTarjeta);

            bool state = Convert.ToBoolean(await command.ExecuteNonQueryAsync());

            return state;
        }
    }
}
