using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Launcher_Core.Exception
{
    /// <summary>
    /// Exception for the Minecraft LauncherConfig Service (MCLS) in case of a ArgumentException
    /// </summary>
    public class MclsArgumentException : ArgumentException
    {
        /// <summary>
        /// Base Exception for the Minecraft LauncherConfig Service (MCLS) in case of a ArgumentException
        /// </summary>
        public MclsArgumentException() : base() { }

        /// <summary>
        /// Exception for the Minecraft LauncherConfig Service (MCLS) in case of a ArgumentException with custom Message
        /// </summary>
        /// <param name="message">Message in the exception</param>
        public MclsArgumentException(string message) : base(message) { }

        /// <summary>
        ///  Exception for the Minecraft LauncherConfig Service (MCLS) in case of a ArgumentException with custom Message and inner Exception
        /// </summary>
        /// <param name="message">Message to show</param>
        /// <param name="inner">Inner Exception</param>
        public MclsArgumentException(string message, System.Exception inner) : base(message, inner) { }
    }
}
