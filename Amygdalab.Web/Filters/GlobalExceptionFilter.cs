using Amygdalab.Core.Exceptions;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Amygdalab.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                var content = GetStatusCode<object>(context.Exception);
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = (int)content.Item2;
                response.ContentType = "application/json";

                context.Result = new JsonResult(content.Item1);
            }
            catch (Exception ex)
            {
                Log.Error("GlobalException  On Exception Logs: " + ex.Message);
            }

        }

        public static (ApiResponse<T> responseModel, HttpStatusCode statusCode) GetStatusCode<T>(Exception exception)
        {
            try
            {
                switch (exception)
                {
                    case BaseException bex:
                        return (new ApiResponse<T>
                        {
                            ResponseCode = bex.Code,
                            ResponseMessage = bex.Message,
                        }, bex.httpStatusCode);
                    case SecurityTokenExpiredException bex:
                        return (new ApiResponse<T>
                        {
                            ResponseCode = Constants.ResponseCodes.TokenExpired,
                            ResponseMessage = "Session expired",
                            RequestSuccessful = false,
                        }, HttpStatusCode.Unauthorized);
                    case ValidationException bex:
                        return (new ApiResponse<T>
                        {
                            ResponseCode = Constants.ResponseCodes.ModelValidation,
                            ResponseMessage = bex.Message,
                            RequestSuccessful = false,
                        }, HttpStatusCode.BadRequest);
                    case SecurityTokenValidationException bex:
                        return (new ApiResponse<T>
                        {
                            ResponseCode = Constants.ResponseCodes.TokenValidationFailed,
                            ResponseMessage = "Invalid authentication parameters",
                            RequestSuccessful = false,
                        }, HttpStatusCode.Unauthorized);
                    default:
                        return (new ApiResponse<T>
                        {
                            ResponseCode = Constants.ResponseCodes.Failed,
                            ResponseMessage = exception.Message,
                            RequestSuccessful = false
                        }, HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                Log.Error("GlobalException Logs: " + ex.Message);

                return (new ApiResponse<T>
                {
                    ResponseCode = Constants.ResponseCodes.Failed,
                    ResponseMessage = exception.Message,
                    RequestSuccessful = false
                }, HttpStatusCode.InternalServerError);
            }

        }

    }
}
