using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class ImageFormatException : Exception
    {
        public ImageFormatException(string msg) : base(msg)
        {

        }
    }
}
