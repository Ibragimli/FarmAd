using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class ValueAlreadyExpception : Exception
    {
        public ValueAlreadyExpception(string msg) : base(msg)
        {

        }
    }
}
