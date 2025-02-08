using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class PaymentValueException : Exception
    {
        public PaymentValueException(string msg) : base(msg)
        {

        }
    }
}
