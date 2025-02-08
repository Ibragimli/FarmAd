using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class SmsSenderException : Exception
    {
        public SmsSenderException(string message) : base(message)
        {
        }
    }
}
