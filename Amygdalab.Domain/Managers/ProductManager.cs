using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Data.Entities;
using Amygdalab.Domain.Interfaces;
using Amygdalab.Domain.Interfaces.Managers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtSetting _settings;

        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtSetting> setting)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = setting.Value;
        }


        public async Task<ApiResponse<Pager<ProductHistoryResponse>>> GetAllProductHistoryAsync(SearchModel model)
        {
            var productHistoryUow = await _unitOfWork.ProductHistory.GetAllProductHistoryAsync(model);

            var ProductHistories = _mapper.Map<Pager<ProductHistoryResponse>>(productHistoryUow);

            return ApiResponse<Pager<ProductHistoryResponse>>.Successful(ProductHistories);
        }

        public async Task<ApiResponse<Pager<ProductResponse>>> GetAllProductAsync(SearchModel model)
        {
            var productUow = await _unitOfWork.Product.GetAllProductAsync(model);

            var products = _mapper.Map<Pager<ProductResponse>>(productUow);

            return ApiResponse<Pager<ProductResponse>>.Successful(products);
        }

        public async Task<ApiResponse<ProductResponse>> GetProductAsync(long productId)
        {
            var productUow = await _unitOfWork.Product.Get(productId);

            if (productUow == null) throw new Exception("Product not found for Id specified");

            var products = _mapper.Map<ProductResponse>(productUow);

            return ApiResponse<ProductResponse>.Successful(products);
        }
        public async Task<ApiResponse<ProductResponse>> CreateProductAsync(ProductRequest model, long createdBy)
        {

            var product = _mapper.Map<Product>(model);
            product.CreatedBy = createdBy;

            _unitOfWork.Product.Add(product);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<ProductResponse>.Successful(null);
        }
        public async Task<ApiResponse<ProductResponse>> EditProductAsync(ProductUpdateRequest model)
        {

            var productUow = await _unitOfWork.Product.Get(model.Id);

            if (productUow == null)
                throw new Exception("Product not found for the Id");

            productUow.ModifiedBy = model.ModifiedBy;
            productUow.Name = model.Name;
            productUow.Description = model.Description;
            productUow.Quantity = model.Quantity;
            productUow.SellingPrice = model.SellingPrice;
            productUow.CostPrice = model.SellingPrice;

            _unitOfWork.Product.Update(productUow);
            _unitOfWork.ProductHistory.Add(new ProductHistory()
            {
                ProductId = model.Id,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CostPrice = model.CostPrice,
                Name = model.Name,
                Quantity = model.Quantity,
                SellingPrice = model.SellingPrice,
                CreatedBy = productUow.CreatedBy,
                CreatedOn = productUow.CreatedOn,
                ModifiedOn = DateTime.UtcNow,
                ModifiedBy = model.ModifiedBy
            });
            await _unitOfWork.CompleteAsync();

            return ApiResponse<ProductResponse>.Successful(null);
        }

        public async Task<ApiResponse<ProductResponse>> DeleteProductAsync(long productId)
        {
            var productUow = await _unitOfWork.Product.Get(productId);

            if (productUow == null) throw new Exception("Product not found for Id specified");

            productUow.IsDeleted = true;

            _unitOfWork.Product.Update(productUow);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<ProductResponse>.Successful(null);
        }
    }
}
