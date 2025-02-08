using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class RareLimitException : Exception
    {
        public RareLimitException(string message) : base(message)
        {
        }
    }
}
