using System;

namespace STAT312WordAnalyzer
{
    public class SessionFileReadException : Exception
    {
        public SessionFileReadException() : base() { }

        public SessionFileReadException(string message) : base(message) { }

        public SessionFileReadException(string message, Exception inner) : base(message, inner) { }
    }
}
