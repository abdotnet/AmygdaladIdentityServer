using Amygdalab.Core.Identity;
using Amygdalab.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.IdentityServices
{
    public class CustomUserManager : UserManager<User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomUserManager(IUserStore<User> store,
       IOptions<IdentityOptions> optionsAccessor,
       IPasswordHasher<User> passwordHasher,
       IEnumerable<IUserValidator<User>> userValidators,
       IEnumerable<IPasswordValidator<User>> passwordValidators,
       ILookupNormalizer keyNormalizer,
       IdentityErrorDescriber errors,
       IServiceProvider services,
       ILogger<UserManager<User>> logger, IUnitOfWork unitOfWork)
           : base(store, optionsAccessor, passwordHasher,
           userValidators, passwordValidators, keyNormalizer,
           errors, services, logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async override Task<IdentityResult> AddClaimAsync(User user, Claim claim)
        {
            UserClaim userClaim = new UserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
            _unitOfWork.UserClaim.Add(userClaim);
            await _unitOfWork.CompleteAsync();

            return await Task.FromResult(IdentityResult.Success);
        }

        public override async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            //get role
            var myroles = await _unitOfWork.Role.Find(x => x.Name == role);

            if (myroles.Count() > 0)
            {
                var myRole = myroles.SingleOrDefault();
                UserRole userRole = new UserRole
                {
                    RoleId = myRole.Id,
                    UserId = user.Id,
                };
                _unitOfWork.UserRole.Add(userRole);
                await _unitOfWork.CompleteAsync();
            }
            return await Task.FromResult(IdentityResult.Success);
        }

    }
}
