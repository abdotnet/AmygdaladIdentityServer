using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Amygdalab.Web.Filters
{
    public class ValidationFilters : Attribute, IAsyncResultFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Before the controller 
            if (!context.ModelState.IsValid)
            {
                var errorsInModelStates = context.ModelState
                    .Where(c => c.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(c => c.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelStates)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorModel()
                        {
                            fieldName = error.Key,
                            message = subError
                        };
                        errorResponse.errors.Add(errorModel);
                    }
                }

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new BadRequestObjectResult(ApiResponse<ErrorResponse>.Error(errorResponse, Constants.ResponseCodes.BadRequest, "Bad Request"));
                await context.Result.ExecuteResultAsync(context);
                return;
            }
            await next();
        }
    }
    public class CustomActionFilter : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Before the controller 
            if (!context.ModelState.IsValid)
            {
                var errorsInModelStates = context.ModelState
                    .Where(c => c.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(c => c.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelStates)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorModel()
                        {
                            fieldName = error.Key,
                            message = subError
                        };
                        errorResponse.errors.Add(errorModel);
                    }
                }

                var apiResponse = new ApiResponse<ErrorResponse>();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new BadRequestObjectResult(ApiResponse<ErrorResponse>.Error(errorResponse,Constants.ResponseCodes.BadRequest, "Bad Request"));
                await context.Result.ExecuteResultAsync(context);
                return;
            }
            await next();
        }


    }
}
