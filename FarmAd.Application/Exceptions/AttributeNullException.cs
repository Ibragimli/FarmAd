using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Exceptions
{
    public class AttributeNullException : Exception
    {
        public AttributeNullException() : base("Attribute is null")
        {
        }

        public AttributeNullException(string? message) : base(message)
        {
        }

        public AttributeNullException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected AttributeNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
