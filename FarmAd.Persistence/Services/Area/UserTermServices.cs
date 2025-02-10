using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Application.Repositories.UserTerm;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminUserTermServices : IAdminUserTermServices
    {
        private readonly IUserTermReadRepository _userTermReadRepository;
        private readonly IUserTermWriteRepository _userTermWriteRepository;

        public AdminUserTermServices(IUserTermReadRepository userTermReadRepository, IUserTermWriteRepository userTermWriteRepository)
        {
            _userTermReadRepository = userTermReadRepository;
            _userTermWriteRepository = userTermWriteRepository;
        }
        public async Task UserTermCreate(UserTerm userTerm)
        {
            UserTerm newUserTerm = new UserTerm();
            bool check = false;
            if (userTerm != null)
            {
                if (await _userTermReadRepository.IsExistAsync(x => x.Title == userTerm.Title))
                    throw new ItemAlreadyException("Bu adda istifadəçi qaydası mövcuddur!");

                if (userTerm.Title == null)
                    throw new ItemNullException("İstifadəçi qaydasının adı boş ola bilməz");
                if (userTerm.Text == null)
                    throw new ItemNullException("İstifadəçi qaydasının mətni boş ola bilməz");

                if (userTerm.Title.Length > 80)
                    throw new ItemFormatException("İstifadəçi qaydasının adının maksimum  uzunluğu 80 ola bilər");

                if (userTerm.Text.Length > 15000)
                    throw new ItemFormatException("İstifadəçi qaydasının mətninin maksimum  uzunluğu 15000 ola bilər");
                newUserTerm.Title = userTerm.Title;
                newUserTerm.Text = userTerm.Text;
                check = true;
            }
            else
                throw new ItemNullException("İstifadəçi qaydası boş ola bilməz");

            if (check)
            {
                await _userTermWriteRepository.AddAsync(newUserTerm);
                await _userTermWriteRepository.SaveAsync();
            }
        }

        public async Task UserTermEdit(UserTerm userTerm)
        {
            bool check = false;
            var oldUserTerm = await _userTermReadRepository.GetAsync(x => x.Id == userTerm.Id);

            if (userTerm.Title != null)
            {
                if (await _userTermReadRepository.IsExistAsync(x => x.Title == userTerm.Title && x.Id != userTerm.Id))
                    throw new ItemAlreadyException("Bu adda istifadəçi qaydası mövcuddur!");


                if (userTerm.Title.Length > 80)
                    throw new ItemFormatException("İstifadəçi qaydasının adının maksimum  uzunluğu 80 ola bilər");

                if (userTerm.Title != oldUserTerm.Title)
                {
                    oldUserTerm.Title = userTerm.Title;
                    check = true;
                }
            }
            else
                throw new ItemNullException("İstifadəçi qaydasının adı boş ola bilməz");


            if (userTerm.Text != null)
            {

                if (userTerm.Text.Length > 15000)
                    throw new ItemFormatException("İstifadəçi qaydasının mətninin maksimum  uzunluğu 15000 ola bilər");


                if (userTerm.Text != oldUserTerm.Text)
                {
                    oldUserTerm.Text = userTerm.Text;
                    check = true;
                }
            }
            else
                throw new ItemNullException("İstifadəçi qaydasının mətni boş ola bilməz");


            if (check)
            {
                oldUserTerm.ModifiedDate = DateTime.UtcNow.AddHours(4);
                await _userTermWriteRepository.SaveAsync();
            }
        }

        public async Task UserTermDelete(int id)
        {
            var oldUserTerm = await _userTermReadRepository.GetAsync(x => x.Id == id);
            _userTermWriteRepository.Remove(oldUserTerm);
            await _userTermWriteRepository.SaveAsync();
        }



        public async Task<UserTerm> GetUserTerm(int id)
        {
            var UserTerm = await _userTermReadRepository.GetAsync(x => x.Id == id && !x.IsDelete);

            return UserTerm;
        }
        public IQueryable<UserTerm> GetUserTerms(string title)
        {
            var userTerm = _userTermReadRepository.AsQueryable();
            userTerm = userTerm.Where(x => !x.IsDelete);
            if (title != null)
                userTerm = userTerm.Where(i => EF.Functions.Like(i.Title, $"%{title}%"));

            return userTerm;
        }
    }
}
