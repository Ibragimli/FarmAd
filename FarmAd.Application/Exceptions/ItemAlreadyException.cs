using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class ItemAlreadyException : Exception
    {
        public ItemAlreadyException(string msg) : base(msg)
        {

        }
    }
}
