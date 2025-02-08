using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{

    public class ImageCountException : Exception
    {
        public ImageCountException(string msg) : base(msg)
        {

        }
    }
}
