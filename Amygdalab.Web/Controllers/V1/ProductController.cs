using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Domain.Interfaces.Managers;
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
    public class ProductController : ControllerBase
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
        [HttpGet()]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProducts([FromBody] SearchModel model)
        {

            Log.Information("Get All product  process started.");

            try
            {
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

                var response = await _productManager.CreateProductAsync(model, 0);

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
        public async Task<IActionResult> CreProduct([FromBody] ProductUpdateRequest model)
        {

            Log.Information("Edit product process started.");

            try
            {
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
        public async Task<IActionResult> EditProduct([FromQuery] long productId)
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
        public async Task<IActionResult> DeleteProduct([FromQuery] long productId)
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
