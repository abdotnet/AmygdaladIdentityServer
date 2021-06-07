using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Interfaces.Managers
{
    public interface IProductManager
    {
        Task<ApiResponse<Pager<ProductResponse>>> GetAllProductAsync(SearchModel model);
        Task<ApiResponse<ProductResponse>> GetProductAsync(long productId);
        Task<ApiResponse<ProductResponse>> CreateProductAsync(ProductRequest model, long createdBy);
        Task<ApiResponse<ProductResponse>> EditProductAsync(ProductUpdateRequest model);
        Task<ApiResponse<ProductResponse>> DeleteProductAsync(long productId);
    }
}
