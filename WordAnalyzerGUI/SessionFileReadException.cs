using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAnalyzerGUI
{
    public class SessionFileReadException : Exception
    {
        public SessionFileReadException() : base() { }

        public SessionFileReadException(string message) : base(message) { }

        public SessionFileReadException(string message, Exception inner) : base(message, inner) { }
    }
}
