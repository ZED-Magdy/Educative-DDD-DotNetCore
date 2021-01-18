using System;

namespace Educative.Domain.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException() : base() { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, Exception inner) : base(message, inner) { }

    }
}
