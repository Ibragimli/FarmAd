using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class CookieNotActiveException : Exception
    {
        public CookieNotActiveException(string msg) : base(msg)
        {

        }
    }
}
