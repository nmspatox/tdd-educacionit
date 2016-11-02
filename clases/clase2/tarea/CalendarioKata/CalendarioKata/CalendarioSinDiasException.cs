using System;
using System.Runtime.Serialization;

namespace CalendarioKata
{
    [Serializable]
    internal class CalendarioSinDiasException : Exception
    {
        public CalendarioSinDiasException()
        {
        }

        public CalendarioSinDiasException(string message) : base(message)
        {
        }

        public CalendarioSinDiasException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CalendarioSinDiasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}