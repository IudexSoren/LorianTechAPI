using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Models
{
    public class Generic_ResultSet<T> : StandardResultObject
    {
        public T ResultSet { get; set; }
    }
}
