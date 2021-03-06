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
    public interface IProductHistoryRepository : IRepository<ProductHistory>
    {
        Task<Pager<ProductHistory>> GetAllProductHistoryAsync(SearchModel model);
    }
}
