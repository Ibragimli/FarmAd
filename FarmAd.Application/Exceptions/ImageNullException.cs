using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class ImageNullException : Exception
    {
        public ImageNullException(string msg) : base(msg)
        {

        }
    }
}
