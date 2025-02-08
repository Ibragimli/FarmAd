using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class ItemFormatException : Exception
    {
        public ItemFormatException(string msg) : base(msg)
        {

        }
    }
}
