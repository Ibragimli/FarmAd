using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IPhoneNumberServices
    {
        string PhoneNumberFilter(string phoneNumber);
        void PhoneNumberValidation(string phoneNumber);
        void PhoneNumberPrefixValidation(string phoneNumber);


    }
}
