using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DAL.Database
{
    internal class FactoryConnection
    {
        public static string CreateConnectionString()
        {
            return GetConnectionString();
        }

        public static string CreateConnectionString(string connectionName)
        {
            return GetConnectionString(connectionName);
        }

        private static string GetConnectionString(string connectionName = "LTConnection")
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            IConfigurationRoot root = configBuilder.Build();
            IConfigurationSection appSettings = root.GetSection($"ConnectionStrings:{ connectionName }");
            string sqlConnectionString = appSettings.Value;

            return sqlConnectionString;
        }
    }
}
