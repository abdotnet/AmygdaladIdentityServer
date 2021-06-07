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
    public interface IAccountManager
    {
        Task<ApiResponse<RegisterResponse>> CreateUser(RegisterRequest model, string roleName= Constants.RoleTypes.Worker);
    }
}
