using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{

    public class UserPasswordResetException : Exception
    {
        public UserPasswordResetException(string msg) : base(msg)
        {

        }
    }
}
