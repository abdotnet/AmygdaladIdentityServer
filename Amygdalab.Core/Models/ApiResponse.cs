using Amygdalab.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Models
{
    public class ApiResponse<TEntity>
    {
        public TEntity ResponseData { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public bool RequestSuccessful { get; set; }

        public static ApiResponse<TEntity> Successful(TEntity model, string status = Constants.ResponseCodes.Successful, string message = "Successful")
        {
            ApiResponse<TEntity> response = new ApiResponse<TEntity>();
            response.ResponseData = model;
            response.ResponseCode = status;
            response.ResponseMessage = message;
            response.RequestSuccessful = true;
            return response;
        }

        public static ApiResponse<TEntity> Error(TEntity model, string status = Constants.ResponseCodes.Failed, string message = "An error occured")
        {
            ApiResponse<TEntity> response = new ApiResponse<TEntity>();
            response.ResponseData = model;
            response.ResponseCode = status;
            response.ResponseMessage = message;
            response.RequestSuccessful = false;

            return response;
        }

    }
}
