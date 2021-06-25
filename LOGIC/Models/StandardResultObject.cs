using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Models
{
    public class StandardResultObject
    {
        public bool Success { get; set; }
        public string UserMessage { get; set; }
        internal string InternalMessage { get; set; }
        internal Exception Exception { get; set; }

        public StandardResultObject()
        {
            this.Success = false;
            this.UserMessage = string.Empty;
            this.InternalMessage = string.Empty;
            this.Exception = null;
        }
    }
}
