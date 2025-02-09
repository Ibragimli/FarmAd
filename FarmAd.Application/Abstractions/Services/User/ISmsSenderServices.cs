using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface ISmsSenderServices
    {
        public Task<bool> SmsSend(string number, string code);

    }
}
