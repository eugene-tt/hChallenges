using System;

namespace Challenge2.Data.Exceptions
{
    public class HprException : Exception
    {
        public HprException(string message) : base(message) { }

        public HprException(string message, Exception innerException) : base(message, innerException) { }
    }
}
