using FarmAd.Application.DTOs.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminLoginServices
    {
        Task<bool> Login(AdminLoginPostDto adminLoginPostDto);
        void Logout();
    }
}
