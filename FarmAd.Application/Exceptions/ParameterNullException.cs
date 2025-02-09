using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FarmAd.Application.Exceptions
{
   
    public class ParameterNullException : Exception
    {
        public ParameterNullException(string msg) : base(msg)
        {

        }

        public ParameterNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ParameterNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
