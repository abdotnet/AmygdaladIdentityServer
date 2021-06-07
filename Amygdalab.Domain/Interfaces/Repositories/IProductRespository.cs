using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Pager<Product>> GetAllProductAsync(SearchModel model);

    }
}
