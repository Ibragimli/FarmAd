using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{

    public class ExpirationDateException : Exception
    {
        public ExpirationDateException() : base("Kodun müddəti bitib.Zəhmət olmasa yenidən daxil olun.")
        {

        }
        public ExpirationDateException(string msg) : base(msg)
        {

        }
        
    }
}
