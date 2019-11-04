using System;
using System.Collections.Generic;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    /// <summary>
    /// The exception that is thrown when an expected value/record/object was not found.
    /// </summary>
    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException() : base() { }
        public ValueNotFoundException(string message) : base(message) { }
        public ValueNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
