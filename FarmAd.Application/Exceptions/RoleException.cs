using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Exceptions
{
    public class RoleException : Exception
    {
        public RoleException() : base("Role tapılmadı")
        {
        }

        public RoleException(string? message) : base(message)
        {
        }

        public RoleException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected RoleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
