using FarmAd.Application.Repositories;
using FarmAd.Domain.Entities.Common;
using FarmAd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        public ReadRepository(DataContext dbContext)
        {
            _context = dbContext;
        }
        public DbSet<T> Table => _context.Set<T>();


        public async Task<T> GetAsync(Expression<Func<T, bool>> method, params string[] includes)
        {
            var query = _query(includes);

            return await query.FirstOrDefaultAsync(method);
        }
        public IQueryable<T> GetAll(bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (method != null)
                query = query.Where(method);

            return await query.ToListAsync();
        }
        public IQueryable<T> GetAllPagenatedAsync(int pageIndex, int pageSize, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.Skip(pageIndex * pageSize).Take(pageSize).AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public IQueryable<T> AsQueryable(params string[] includes)
        {
            var query = _query(includes);

            query = _query(includes);
            return query.AsQueryable();
        }

        public async Task<int> GetTotalCountAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.CountAsync();
        }

        public async Task<bool> IsExistAsync(int id, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.AnyAsync(data => data.Id == id);

        }
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.AnyAsync(method);

        }
        public async Task<T> GetByIdAsync(int id, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);

            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == id);
            //=> await Table.FirstOrDefaultAsync(data => data.Id == id);
            //=> await Table.FindAsync(id);
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes)
        {
            var query = _query(includes);
            query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        //=> await Table.FirstOrDefaultAsync(method);
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true, params string[] includes)
        //=> Table.Where(method);
        {
            var query = _query(includes);
            query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        private IQueryable<T> _query(params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
