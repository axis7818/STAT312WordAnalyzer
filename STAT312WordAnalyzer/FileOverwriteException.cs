using System;

namespace STAT312WordAnalyzer
{
    public class FileOverwriteException : Exception
    {
        public FileOverwriteException() : base() { }

        public FileOverwriteException(string message) : base(message) { }

        public FileOverwriteException(string message, Exception inner) : base(message, inner) { }
    }
}
