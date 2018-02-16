using System;
using System.Runtime.Serialization;
using Frameshift;

namespace Frameshift {
    public class CodonNotFoundException : Exception
    {
        public CodonNotFoundException()
        {
        }

        public CodonNotFoundException(string message) : base(message)
        {
        }

        public CodonNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CodonNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
