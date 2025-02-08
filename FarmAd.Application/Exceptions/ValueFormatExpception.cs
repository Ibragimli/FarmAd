using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class ValueFormatExpception : Exception
    {
        public ValueFormatExpception(string msg) : base(msg)
        {

        }
    }
}
