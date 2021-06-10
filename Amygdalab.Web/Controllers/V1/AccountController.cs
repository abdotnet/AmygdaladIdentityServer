using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Core.Identity;
using Amygdalab.Core.IdentityModel;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Domain.IdentityServices;
using Amygdalab.Domain.Interfaces;
using Amygdalab.Domain.Interfaces.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amygdalab.Web.Controllers.V1
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v{version:apiVersion}/account")]
    [ApiVersion("1")]
    [ApiController]
    public class AccountController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountManager _accountManager;
        public AccountController(CustomUserManager userManager, IUnitOfWork unitOfWork,
            RoleManager<Role> roleManager, IAccountManager accountManager)
        {
            _unitOfWork = unitOfWork;
            _accountManager = accountManager;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<RegisterResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<RegisterResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<RegisterResponse>), StatusCodes.Status500InternalServerError)]
        [Route("user")]
        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> RegisterWorker([FromBody] RegisterRequest model)
        {
            Log.Information("Worker registration process started.");

            try
            {
                var response = await _accountManager.CreateUser(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                var error = ApiResponse<RegisterResponse>.Error(null, message: ex.Message);
                return Ok(error);
            }

        }
    }
}
