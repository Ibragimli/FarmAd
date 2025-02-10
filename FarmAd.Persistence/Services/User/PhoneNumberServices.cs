using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FarmAd.Persistence.Service.User
{
    public class PhoneNumberServices : IPhoneNumberServices
    {
        public void PhoneNumberValidation(string phoneNumber)
        {
            if (phoneNumber != null)
            {
                if (Regex.IsMatch(phoneNumber, "[a-zA-Z]"))
                    throw new ItemFormatException("Nömrə yanlışdır");

                if (phoneNumber.Length > 15)
                    throw new ItemFormatException("Nömrə yanlışdır");
            }
        }
        public void PhoneNumberPrefixValidation(string phoneNumber)
        {
            string phoneRegex = @"^(050|051|055|070|077|010)(\d{7})$";
            if (phoneNumber != null)
            {
                if (!Regex.IsMatch(phoneNumber, phoneRegex))
                    throw new ItemFormatException("Nömrənin prefiksi yanlışdır!");
            }
        }

        public string PhoneNumberFilter(string phoneNumber)
        {
            string number = "";
            string[] charsNumber = phoneNumber.Split('_', '-', ' ', '(', ')', ',', '.', '/', '?', '!', '+', '=', '|', '.');

            foreach (var item in charsNumber)
            {
                number += item;
            }
            return number;
        }
    }
}
