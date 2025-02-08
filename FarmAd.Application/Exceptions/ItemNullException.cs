using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
   
    public class ItemNullException : Exception
    {
        public ItemNullException(string msg) : base(msg)
        {

        }
    }
}
