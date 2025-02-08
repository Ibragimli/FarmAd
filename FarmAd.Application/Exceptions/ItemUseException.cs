using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{

    public class ItemUseException : Exception
    {
        public ItemUseException(string msg) : base(msg)
        {

        }
    }
}
