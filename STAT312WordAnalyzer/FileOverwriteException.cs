using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAnalyzerGUI
{
    public class FileOverwriteException : Exception
    {
        public FileOverwriteException() : base() { }

        public FileOverwriteException(string message) : base(message) { }

        public FileOverwriteException(string message, Exception inner) : base(message, inner) { }
    }
}
