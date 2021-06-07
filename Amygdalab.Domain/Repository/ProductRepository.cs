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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pager<Product>> GetAllProductAsync(SearchModel model)
        {
            var query = _context.Products.OrderByDescending(ord => ord.CreatedOn).AsQueryable();

            PagedList<Product> pageData = await PagedList<Product>.CreateAsync(query, model.PageNumber, model.PageSize);

            Pager<Product> pager = new Pager<Product>()
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
