using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class AuthenticationCodeException : Exception
    {
        public AuthenticationCodeException(string msg) : base(msg)
        {

        }
    }
}
