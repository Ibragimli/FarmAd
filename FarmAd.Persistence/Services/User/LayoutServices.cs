using FarmAd.Domain.Entities;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Persistence.Contexts;

namespace FarmAd.Persistence.Service.User
{
    public class LayoutServices : ILayoutServices
    {

        public Task<List<City>> GetAllCitiesSearch()
        {
            throw new NotImplementedException();
        }

        public Task<List<Setting>> GetSettingsAsync()
        {
            throw new NotImplementedException();
        }
        //private readonly DataContext _context;

        //public LayoutServices(DataContext context)
        //{
        //    _context = context;
        //}
        //public async Task<List<Setting>> GetSettingsAsync()
        //{
        //    return await _context.Settings.ToListAsync();
        //}
        //public async Task<List<City>> GetAllCitiesSearch()
        //{
        //    return await _context.Cities.ToListAsync();
        //}

    }
}
