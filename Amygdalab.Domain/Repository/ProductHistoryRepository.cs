using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Data;
using Amygdalab.Data.Entities;
using Amygdalab.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Repository
{
    public class ProductHistoryRepository : BaseRepository<ProductHistory>, IProductHistoryRepository
    {
        private readonly DataContext _context;
        public ProductHistoryRepository(DataContext context) : base(context)
        {
            _context = context;
           
        }

        public async Task<Pager<ProductHistory>> GetAllProductHistoryAsync(SearchModel model)
        {
            var query = _context.ProductHistories.OrderByDescending(ord => ord.CreatedOn).AsQueryable();
          
            PagedList<ProductHistory> pageData = await PagedList<ProductHistory>.CreateAsync(query, model.PageNumber, model.PageSize);

            Pager<ProductHistory> pager = new Pager<ProductHistory>()
            {
                CurrentPage = pageData.CurrentPage,
                Result = pageData,
                ItemsPerPage = pageData.PageSize,
                TotalItems = pageData.TotalCount,
                TotalPages = pageData.TotalPages,
            };

            return pager;
        }

    }
}
