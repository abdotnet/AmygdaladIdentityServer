using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Domain.Interfaces.Managers;
using Amygdalab.Web.Extensions.Identity;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Amygdalab.Web.Controllers.V1
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("1")]
    [ApiController]
    public class ProductController : BaseController
    {

        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;

        }

        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 400)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 401)]
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProductsByAdminAsync([FromQuery] SearchModel model)
        {

            Log.Information("Get All product history process started.");

            try
            {
                var response = await _productManager.GetAllProductHistoryAsync(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex.Message);
                return Ok(error);
            }

        }

        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 400)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 401)]
        [HttpGet()]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> GetAllProductAsync([FromQuery] SearchModel model)
        {

            Log.Information("Get All product by user process started.");

            try
            {
                model.UserId = User.GetUserId();
                var response = await _productManager.GetAllProductAsync(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex.Message);
                return Ok(error);
            }

        }

        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 400)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 401)]
        [HttpPost]
        [Authorize(Roles = Constants.RoleTypes.Worker)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest model)
        {

            Log.Information("Create product process started.");

            try
            {

                var response = await _productManager.CreateProductAsync(model, User.GetUserId());

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex.Message);
                return Ok(error);
            }



        }

        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 400)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 401)]
        [HttpPut]
        [Authorize(Roles = Constants.RoleTypes.Worker)]
        public async Task<IActionResult> EditProduct([FromBody] ProductUpdateRequest model)
        {

            Log.Information("Edit product process started.");

            try
            {
                model.ModifiedBy = User.GetUserId();
                model.ModifiedOn = DateTime.Now;

                var response = await _productManager.EditProductAsync(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex?.Message);
                return Ok(error);
            }

        }
        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 400)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 401)]
        [HttpGet("{productId}")]
        [Authorize(Roles = Constants.RoleTypes.Worker)]
        public async Task<IActionResult> GetProduct(long productId)
        {

            Log.Information("Edit product process started.");

            try
            {
                var response = await _productManager.GetProductAsync(productId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex?.Message);
                return Ok(error);
            }

        }

        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 400)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductResponse>>), 401)]
        [HttpDelete("{productId}")]
        [Authorize(Roles = Constants.RoleTypes.Worker)]
        public async Task<IActionResult> DeleteProduct(long productId)
        {

            Log.Information("Delete product process started.");

            try
            {
                var response = await _productManager.DeleteProductAsync(productId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex?.Message);
                return Ok(error);
            }

        }

    }
}
