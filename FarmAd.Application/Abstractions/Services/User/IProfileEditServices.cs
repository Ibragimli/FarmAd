using FarmAd.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProfileEditServices
    {
        public Task CheckValue(ProfileEditDto editDto);
        public Task Edit(ProfileEditDto editDto);
        public Task<ProductEditGetDto> EditVM(int id);
    }
}
