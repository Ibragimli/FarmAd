using FarmAd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Expression<Func<T, bool>> method, params string[] includes);
        IQueryable<T> GetAll(bool tracking = true, params string[] includes);
        //public Task<List<T>> GetAllAsync(bool tracking = true);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes);
        IQueryable<T> AsQueryable(params string[] includes);

        IQueryable<T> GetAllPagenated(int pageIndex, int pageSize, bool tracking = true, params string[] includes);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes);
        Task<int> GetTotalCountAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes);
        Task<T> GetByIdAsync(int id, bool tracking = true, params string[] includes);
        Task<bool> IsExistAsync(int id, bool tracking = true, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes);


    }
}
