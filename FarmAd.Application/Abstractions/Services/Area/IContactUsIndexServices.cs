using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IContactUsIndexServices
    {
        Task<(object, int)> SearchCheck(string search, int page, int size);

    }
}
