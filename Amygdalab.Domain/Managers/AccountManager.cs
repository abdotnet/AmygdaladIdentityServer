using Amygdalab.Core.Contracts.Request;
using Amygdalab.Core.Contracts.Response;
using Amygdalab.Core.Identity;
using Amygdalab.Core.Models;
using Amygdalab.Core.Utilities;
using Amygdalab.Domain.IdentityServices;
using Amygdalab.Domain.Interfaces.Managers;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly CustomUserManager _userManager;
        private readonly RoleManager<Role> _roleManager;
        public AccountManager(CustomUserManager userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<RegisterResponse>> CreateUser(RegisterRequest model)
        {
            var userEmail = await _userManager.FindByEmailAsync(model.EmailAddress.ToLower().Trim());

            if (userEmail != null)
                throw new Exception("User email not found");

            var user = await _userManager.FindByNameAsync(model.UserName.ToLower().Trim());

            if (user != null)
                throw new Exception("User not found");

            if (user == null)
            {
                user = new User
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName =model.LastName,
                    NormalizedUserName = model.UserName,
                    Email = model.EmailAddress,
                    EmailConfirmed = false,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                Log.Information("User created:- Name: " + user.UserName + ", Id: " + user.Id);


                Role role = new Role();
                role.Name = model.role;

                if (result.Succeeded)
                {
                    var _role = await _roleManager.FindByNameAsync(model.role);
                    if (_role == null)
                    {
                        await _roleManager.CreateAsync(role);
                        Log.Information("Role created:- Name: " + role.Name + ", Id: " + role.Id);
                    }

                    // assign role to user 
                    await _userManager.AddToRoleAsync(user, model.role);

                    // Add Claims 
                    await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
                    await _userManager.AddClaimAsync(user, new Claim("FirstName", user.FirstName));
                    await _userManager.AddClaimAsync(user, new Claim("EmailAddress", user.Email));
                    await _userManager.AddClaimAsync(user, new Claim("Role", model.role));
                    await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id.ToString()));
                    Log.Information("Claims added");
                }
                else
                    throw new Exception("User not created!");

            }
            return ApiResponse<RegisterResponse>.Successful(null);
        }
    }
}
