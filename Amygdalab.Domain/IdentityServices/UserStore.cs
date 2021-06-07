using Amygdalab.Core.Identity;
using Amygdalab.Domain.Interfaces;
using Amygdalab.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Services
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>, IUserEmailStore<User>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _unitOfWork.User.Add(user);
            await _unitOfWork.CompleteAsync();

            return await Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                var user = await _unitOfWork.User.Get(id);
                if (user!=null)
                {
                    return user;
                }
                else
                {
                    return await Task.FromResult((User)null);
                }
            }
            else
            {
                return await Task.FromResult((User)null);
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.User.FindUserByUserName(normalizedUserName);
                return users.SingleOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(user.PasswordHash);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var userRoles = await _unitOfWork.UserRole.Find(x => x.UserId == user.Id);
            IList<string> roles = new List<string>();
            foreach (UserRole r in userRoles)
            {
                var userroles = await _unitOfWork.Role.Find(x => x.Id == r.RoleId);
                roles.Add(userroles.FirstOrDefault().Name);
            }
            return await Task.FromResult(roles);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult((object)null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(nameof(SetUserNameAsync));
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _unitOfWork.User.Update(user);
            await _unitOfWork.CompleteAsync();
            return await Task.FromResult(IdentityResult.Success);
        }


        public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var userList = await _unitOfWork.User.Find(x => x.Email == email);
                if (userList.Count() != 0)
                {
                    return userList.FirstOrDefault();
                }
                else
                {
                    return await Task.FromResult((User)null);
                }
            }
            else
            {
                return await Task.FromResult((User)null);
            }
        }
        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            //get role
            var myroles = await _unitOfWork.Role.Find(x => x.Name == role);

            if (myroles.Count() > 0)
            {
                var myRole = myroles.SingleOrDefault();
                UserRole userRole = new UserRole
                {
                    RoleId = myRole.Id,
                    UserId = user.Id
                };
                _unitOfWork.UserRole.Add(userRole);
                await _unitOfWork.CompleteAsync();
            }
            return await Task.FromResult(IdentityResult.Success);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToString());
            //throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }
    }
}
