using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Launcher_Core.Exception
{
    public class MclsArgumentException : ArgumentException
    {
        public MclsArgumentException() : base() { }
        
        public MclsArgumentException(string message) : base(message) { }
        
        public MclsArgumentException(string message, System.Exception inner) : base(message, inner) { }
    }
}
