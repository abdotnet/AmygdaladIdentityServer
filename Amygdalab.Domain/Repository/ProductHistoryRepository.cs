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

    }
}
