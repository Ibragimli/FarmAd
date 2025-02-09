using FarmAd.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IContactUsServices
    {
        public void CheckContactUs(ContactUsDto contactUsDto);
        public Task CreateContactUs(ContactUsDto contactUsDto);
    }
}
