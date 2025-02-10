using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Abstractions.Services
{
    public interface IEmailServices
    {
        public void Send(string to, string subject, string html);
    }
}
