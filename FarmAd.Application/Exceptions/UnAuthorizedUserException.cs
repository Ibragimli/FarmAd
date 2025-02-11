using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Exceptions
{
    public class UnauthorizedUserException : Exception
    {

        public UnauthorizedUserException() : base("Uğursuz cəhd.Zəhmət olmasa yenidən daxil olun.")
        {
        }

        public UnauthorizedUserException(string? message) : base(message)
        {
        }
    }
}
