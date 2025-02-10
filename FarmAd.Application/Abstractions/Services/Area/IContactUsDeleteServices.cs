using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IContactUsDeleteServices
    {
        Task ContactUsDelete(int id);
    }
}
