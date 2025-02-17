using FarmAd.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Helpers
{
    public static class PhoneNumberHelper
    {
        public static void PhoneNumberValidation(string phoneNumber)
        {
            if (phoneNumber != null)
            {
                if (Regex.IsMatch(phoneNumber, "[a-zA-Z]"))
                    throw new ItemFormatException("Nömrə yanlışdır");

                if (phoneNumber.Length > 15 || phoneNumber.Length < 9)
                    throw new ItemFormatException("Nömrə uzunluğu düzgün deyil!");
            }
        }
        public static void PhoneNumberPrefixValidation(string phoneNumber)
        {
            string phoneRegex = @"^(050|051|055|070|077|010)(\d{7})$";
            if (phoneNumber != null)
            {
                if (!Regex.IsMatch(phoneNumber, phoneRegex))
                    throw new ItemFormatException("Nömrənin prefiksi yanlışdır!");
            }
        }

        public static string PhoneNumberFilter(string phoneNumber)
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
