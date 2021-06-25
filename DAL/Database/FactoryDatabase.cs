using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using DAL.Interfaces;

namespace DAL.Database
{
    internal class FactoryDatabase
    {
        public static IDataBase CreateDatabaseObject()
        {
            try
            {
                IDbConnection connection = null;
                string connectionString = FactoryConnection.CreateConnectionString();
                IDataBase database = new DataBase();
                connection = new SqlConnection(connectionString);
                connection.Open();
                database.Connection = connection;

                if (connection.State != ConnectionState.Open)
                {
                    throw new Exception("La conexión con la base de datos ha fracasado.");
                }

                return database;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
