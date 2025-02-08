using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string msg) : base(msg)
        {

        }
    }
}
