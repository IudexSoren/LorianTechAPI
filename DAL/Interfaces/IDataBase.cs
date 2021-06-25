using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL.Interfaces
{
    public interface IDataBase : IDisposable
    {
        IDbConnection Connection { get; set; }
    }
}
