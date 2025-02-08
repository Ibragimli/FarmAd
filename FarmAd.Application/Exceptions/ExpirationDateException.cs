using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{

    public class ExpirationDateException : Exception
    {
        public ExpirationDateException(string msg) : base(msg)
        {

        }
    }
}
