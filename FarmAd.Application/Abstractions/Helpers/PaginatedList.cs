using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.AddRange(items);
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IQueryable<T> query, int pageIndex, int pageSize)
        {
            int totalCount = query.Count();
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, totalCount, pageIndex, pageSize);
        }

        public static PaginatedList<T> CreateRandom(IQueryable<T> query, int pageIndex, int pageSize)
        {
            int totalCount = query.Count();
            var items = new List<T>();

            if (totalCount > pageSize)
            {
                var rand = new Random();
                int skipIndex = rand.Next(0, totalCount - pageSize);
                items = query.Skip(skipIndex).Take(pageSize).ToList();
            }
            else 
                items = query.ToList();

            return new PaginatedList<T>(items, totalCount, pageIndex, pageSize);
        }
    }

}
