using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL.Database
{
    internal class DataBase : IDataBase
    {
        public IDbConnection Connection { get; set; }

        public void Dispose()
        {
            if (Connection != null)
                Connection.Close();
        }
    }
}
